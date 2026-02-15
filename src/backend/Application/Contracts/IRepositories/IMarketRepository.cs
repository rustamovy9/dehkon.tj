using System.Linq.Expressions;
using Application.Contracts.IRepositories.IBaseRepository;
using Application.Extensions.ResultPattern;
using Domain.Entities;

namespace Application.Contracts.IRepositories;

public interface IMarketRepository : IGenericRepository<Market>
{
    Task<Result<Market>> GetByIdWithSellersAsync(int id);

    Task<Result<IEnumerable<Market>>> GetAllWithSellersAsync(Expression<Func<Market, bool>> expression);
}