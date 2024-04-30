using MediatR;
using Microservice.Product.Application.Commons.Bases;

namespace Microservice.Product.Application.UseCases.Product.Commands.UpdateCommand;

public class UpdateProductCommand : IRequest<BaseResponse<bool>>
{
    public int ProductId { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int StockMin { get; set; }
    public int StockMax { get; set; }
    public decimal UnitSalePrice { get; set; }
    public int State { get; set; }
}