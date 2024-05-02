using Microservice.Product.Application.Interfaces.Events;
using Microservice.Product.Application.UseCases.Product.Commands.CreateEventCommand;
using Microservice.Product.Domain.Commands;
using Taller.Microservices.Domain.Bus;

namespace Microservice.Product.Application.UseCases.Product.Events;

public class ProductEventBus : IProductEventBus
{
    private readonly IEventBus _eventBus;

    public ProductEventBus(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void OrderProduct(ProductCommand productCommand)
    {
        var order = new CreateOrderProductCommand(productCommand);
        _eventBus.SendCommand(order);
    }
}