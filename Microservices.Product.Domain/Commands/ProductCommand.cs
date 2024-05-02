using Taller.Microservices.Domain.Commands;

namespace Microservice.Product.Domain.Commands;

public abstract class ProductCommand : Command
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int StockMin { get; set; }
    public int StockMax { get; set; }
    public decimal UnitSalePrice { get; set; }
    public int State { get; set; }
}