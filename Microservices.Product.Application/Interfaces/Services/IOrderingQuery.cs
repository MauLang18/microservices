using Microservice.Product.Application.Commons.Bases;

namespace Microservice.Product.Application.Interfaces.Services;

public interface IOrderingQuery
{
    IQueryable<T> Ordering<T>(BasePagination request, IQueryable<T> queryable) where T : class;
}