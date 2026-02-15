using System.Linq.Expressions;
using Application.Contracts.IRepositories.IBaseRepository;
using Application.Extensions.ResultPattern;
using Domain.Entities;

namespace Application.Contracts.IRepositories;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<Result<IEnumerable<Product>>> 
        FindWithSellerAsync(Expression<Func<Product, bool>> expression);
}