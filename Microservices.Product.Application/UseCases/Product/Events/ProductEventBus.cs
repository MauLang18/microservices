using Microservice.Product.Application.Dtos.Product.Request;
using Microservice.Product.Application.Interfaces.Events;
using Microservice.Product.Application.UseCases.Product.Commands.CreateEventCommand;
using Taller.Microservices.Domain.Bus;

namespace Microservice.Product.Application.UseCases.Product.Events;

public class ProductEventBus : IProductEventBus
{
    private readonly IEventBus _eventBus;

    public ProductEventBus(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void OrderProduct(ProductRequestDto productCommand)
    {
        var order = new CreateOrderProductCommand(
            productCommand.Code, 
            productCommand.Name, 
            productCommand.StockMin, 
            productCommand.StockMax, 
            productCommand.UnitSalePrice, 
            productCommand.State);

        _eventBus.SendCommand(order);
    }
}