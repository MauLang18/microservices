using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Taller.Microservices.Domain.Bus;
using Taller.Microservices.Domain.Commands;
using Taller.Microservices.Domain.Events;

namespace Taller.Microservices.EventBus;

public sealed class RabbitMQEventBus : IEventBus
{
    private readonly IMediator _mediator;
    private readonly Dictionary<string, List<Type>> _handlers;
    private readonly List<Type> _events;
    private readonly RabbitMQSettings _settings;

    public RabbitMQEventBus(IOptions<RabbitMQSettings> settings, IMediator mediator)
    {
        _mediator = mediator;
        _handlers = new Dictionary<string, List<Type>>();
        _events = new List<Type>();
        _settings = settings.Value;
    }

    public void Publish<T>(T @event) where T : Event
    {
        var factory = new ConnectionFactory
        {
            HostName = _settings.HostName,
            UserName = _settings.UserName,
            Password = _settings.Password,
        };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            var eventName = @event.GetType().Name;
            channel.QueueDeclare(eventName, false, false, false, null);

            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish("", eventName, null, body);
        }
    }

    public Task SendCommand<T>(T command) where T : Command
    {
        return _mediator.Send(command);
    }

    public void Subscribe<T, TH>()
        where T : Event
        where TH : IEventHandler<T>
    {
        var eventName = typeof(T).Name;
        var handler = typeof(TH);

        if (!_events.Contains(typeof(T)))
        {
            _events.Add(typeof(T));
        }

        if (!_handlers.ContainsKey(eventName))
        {
            _handlers.Add(eventName, new List<Type>());
        }

        if (_handlers[eventName].Any(x => x.GetType() == handler))
        {
            throw new 
                ArgumentException($"Handler exception {handler.Name} already exists By {eventName}.", nameof(handler));
        }

        _handlers[eventName].Add(handler);

        InitBasicConsumer<T>();
    }

    private void InitBasicConsumer<T>() where T : Event
    {
        var factory = new ConnectionFactory
        {
            HostName = _settings.HostName,
            UserName = _settings.UserName,
            Password = _settings.Password,
            DispatchConsumersAsync = true,
        };

        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        var eventName = typeof(T).Name;

        channel.QueueDeclare(eventName, false, false, false, null);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.Received += ConsumerReceived;

        channel.BasicConsume(eventName, true, consumer);
    }

    private async Task ConsumerReceived(object sender, BasicDeliverEventArgs eventArgs)
    {
        var eventName = eventArgs.RoutingKey;
        var message = Encoding.UTF8.GetString(eventArgs.Body.Span);

        try
        {
            await ProcessEvent(eventName, message).ConfigureAwait(false);
        }
        catch (Exception ex)
        {

        }
    }

    private async Task ProcessEvent(string eventName, string message)
    {
        if (_handlers.ContainsKey(eventName))
        {
            var subs = _handlers[eventName];

            foreach (var sub in subs) 
            {
                var handler = Activator.CreateInstance(sub);

                if (handler is null) continue;

                var eventType = _events.SingleOrDefault(x => x.Name == eventName);
                var @event = JsonConvert.DeserializeObject(message, eventType!);
                var concrete = typeof(IEventHandler<>).MakeGenericType(eventType!);

                await ((Task)concrete.GetMethod("Handle")!.Invoke(handler, [@event])!).ConfigureAwait(false);
            }
        }
    }
}