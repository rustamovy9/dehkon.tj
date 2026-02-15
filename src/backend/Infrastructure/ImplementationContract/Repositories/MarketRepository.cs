using System.Linq.Expressions;
using System.Security.AccessControl;
using Application.Contracts.IRepositories;
using Application.Extensions.ResultPattern;
using Domain.Common;
using Domain.Entities;
using Infrastructure.DataAccess;
using Infrastructure.ImplementationContract.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ImplementationContract.Repositories;

public class MarketRepository(DataContext dbContext) : GenericRepository<Market>(dbContext), IMarketRepository
{
    private readonly DataContext _dbContext = dbContext;

    public async Task<Result<Market>> GetByIdWithSellersAsync(int id)
    {
        try
        {
            var market = await _dbContext.Markets
                .Include(m => m.Sellers)
                .FirstOrDefaultAsync(m => m.Id == id);

            return market != null
                ? Result<Market>.Success(market)
                : Result<Market>.Failure(Error.NotFound());
        }
        catch (Exception e)
        {
            return Result<Market>.Failure(
                Error.InternalServerError(e.Message));
        }
    }
    
    public async Task<Result<IEnumerable<Market>>> 
        GetAllWithSellersAsync(Expression<Func<Market, bool>> expression)
    {
        try
        {
            var markets = await _dbContext.Markets
                .Include(m => m.Sellers)
                .Where(expression)
                .ToListAsync();

            return Result<IEnumerable<Market>>.Success(markets);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<Market>>.Failure(
                Error.InternalServerError(e.Message));
        }
    }
    
}