using Microservice.Product.Application.Interfaces.Persistence;
using Entity = Microservice.Product.Domain.Entities;

namespace Microservice.Product.Application.Interfaces.Services;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Entity.Product> Product { get; }
    Task SaveChangeAsync();
}