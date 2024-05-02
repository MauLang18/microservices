using Taller.Microservices.Domain.Events;

namespace Microservice.Product.Domain.Events;

public class CreateOrderProductEvent : Event
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int StockMin { get; set; }
    public int StockMax { get; set; }
    public decimal UnitSalePrice { get; set; }
    public int State { get; set; }

    public CreateOrderProductEvent(string code, string name, int stockMin, int stockMax, decimal unitSalePrice, int state)
    {
        Code = code;
        Name = name;
        StockMin = stockMin;
        StockMax = stockMax;
        UnitSalePrice = unitSalePrice;
        State = state;
    }
}