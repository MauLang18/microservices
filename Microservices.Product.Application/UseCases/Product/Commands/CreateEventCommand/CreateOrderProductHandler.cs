using MediatR;
using Microservice.Product.Domain.Events;
using Taller.Microservices.Domain.Bus;

namespace Microservice.Product.Application.UseCases.Product.Commands.CreateEventCommand;

public class CreateOrderProductHandler : IRequestHandler<CreateOrderProductCommand, bool>
{
    private readonly IEventBus _eventBus;

    public CreateOrderProductHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public Task<bool> Handle(CreateOrderProductCommand request, CancellationToken cancellationToken)
    {
        _eventBus.Publish(new CreateOrderProductEvent(
            request.Code, 
            request.Name, 
            request.StockMin, 
            request.StockMax, 
            request.UnitSalePrice, 
            request.State));

        return Task.FromResult(true);
    }
}