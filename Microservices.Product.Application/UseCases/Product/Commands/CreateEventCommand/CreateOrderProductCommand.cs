using MediatR;
using Microservice.Product.Domain.Commands;

namespace Microservice.Product.Application.UseCases.Product.Commands.CreateEventCommand;

public class CreateOrderProductCommand(ProductCommand productCommand) : ProductCommand, IRequest<bool>
{
    public ProductCommand ProductCommand { get; } = productCommand;
}