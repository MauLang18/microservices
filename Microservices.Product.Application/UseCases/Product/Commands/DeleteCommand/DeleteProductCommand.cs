using MediatR;
using Microservice.Product.Application.Commons.Bases;

namespace Microservice.Product.Application.UseCases.Product.Commands.DeleteCommand;

public class DeleteProductCommand : IRequest<BaseResponse<bool>>
{
    public int ProductId { get; set; }
}