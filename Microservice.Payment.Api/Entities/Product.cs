namespace Microservice.Payment.Api.Entities;

public class Product
{
    public int ProductId { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int StockMin { get; set; }
    public int StockMax { get; set; }
    public decimal UnitSalePrice { get; set; }
}