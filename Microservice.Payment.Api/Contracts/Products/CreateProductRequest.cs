namespace Microservice.Payment.Api.Contracts.Products;

public class CreateProductRequest
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int StockMin { get; set; }
    public int StockMax { get; set; }
    public decimal UnitSalePrice { get; set; }
}