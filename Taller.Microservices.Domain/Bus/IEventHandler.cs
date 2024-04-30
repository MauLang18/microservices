using Taller.Microservices.Domain.Events;

namespace Taller.Microservices.Domain.Bus;

public interface IEventHandler<TEvent> : IEventHandler where TEvent : Event
{
    Task Handle(TEvent @event);
}

public interface IEventHandler { }