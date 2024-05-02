using Microservice.Product.Domain.Commands;

namespace Microservice.Product.Application.Interfaces.Events;

public interface IProductEventBus
{
    void OrderProduct(ProductCommand productCommand);
}