using Microservice.Product.Domain.Entities;

namespace Microservice.Product.Application.Interfaces.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task CreateAsync(T entity);
    void UpdateAsync(T entity);
    Task DeleteAsync(int id);
}