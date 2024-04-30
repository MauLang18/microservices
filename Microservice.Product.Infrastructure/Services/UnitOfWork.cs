using Microservice.Product.Application.Interfaces.Persistence;
using Microservice.Product.Application.Interfaces.Services;
using Microservice.Product.Infrastructure.Persistence.Context;
using Microservice.Product.Infrastructure.Persistence.Repositories;
using Entity = Microservice.Product.Domain.Entities;

namespace Microservice.Product.Infrastructure.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly IGenericRepository<Entity.Product> _product = null!;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<Entity.Product> Product => _product ?? new GenericRepository<Entity.Product>(_context);

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task SaveChangeAsync() => await _context.SaveChangesAsync();
}