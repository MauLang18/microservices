using MediatR;
using Microservice.Product.Application.Commons.Bases;
using Microservice.Product.Application.Dtos.Product.Response;

namespace Microservice.Product.Application.UseCases.Product.Queries.GetByIdQuery;

public class GetProductByIdQuery : IRequest<BaseResponse<ProductByIdResponseDto>>
{
    public int ProductId { get; set; }
}