namespace Microservice.Product.Application.Dtos.Product.Response;

public class ProductResponseDto
{
    public int ProductId { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int StockMin { get; set; }
    public int StockMax { get; set; }
    public decimal UnitSalePrice { get; set; }
    public int State { get; set; }
    public string StateProduct { get; set; } = null!;
}