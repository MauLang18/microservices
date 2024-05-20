namespace Microservice.Product.Application.Dtos.Product.Request;

public class ProductRequestDto
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int StockMin { get; set; }
    public int StockMax { get; set; }
    public decimal UnitSalePrice { get; set; }
    public int State { get; set; }
}