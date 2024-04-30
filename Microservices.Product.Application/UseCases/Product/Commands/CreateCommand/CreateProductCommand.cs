using MediatR;
using Microservice.Product.Application.Commons.Bases;

namespace Microservice.Product.Application.UseCases.Product.Commands.CreateCommand;

public class CreateProductCommand : IRequest<BaseResponse<bool>>
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int StockMin { get; set; }
    public int StockMax { get; set; }
    public decimal UnitSalePrice { get; set; }
    public int State { get; set; }
}