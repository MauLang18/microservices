using Microservice.Product.Domain.Commands;

namespace Microservice.Product.Application.UseCases.Product.Commands.CreateEventCommand;

public class CreateOrderProductCommand : ProductCommand
{
    public CreateOrderProductCommand(string code, string name, int stockMin, int stockMax, decimal unitSalePrice, int state)
    {
        Code = code;
        Name = name;
        StockMin = stockMin;
        StockMax = stockMax;
        UnitSalePrice = unitSalePrice;
        State = state;
    }
}