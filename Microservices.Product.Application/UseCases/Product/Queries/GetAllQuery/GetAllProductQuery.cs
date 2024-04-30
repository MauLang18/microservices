using MediatR;
using Microservice.Product.Application.Commons.Bases;
using Microservice.Product.Application.Dtos.Product.Response;

namespace Microservice.Product.Application.UseCases.Product.Queries.GetAllQuery;

public class GetAllProductQuery : IRequest<BaseResponse<IEnumerable<ProductResponseDto>>>
{
}