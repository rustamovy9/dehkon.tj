using System.Linq.Expressions;
using Application.Contracts.IRepositories;
using Application.Extensions.ResultPattern;
using Domain.Common;
using Domain.Entities;
using Infrastructure.DataAccess;
using Infrastructure.ImplementationContract.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ImplementationContract.Repositories;

public class ProductRepository(DataContext dbContext)
: GenericRepository<Product>(dbContext),IProductRepository
{
    private readonly DataContext _dbContext = dbContext;

    public async Task<Result<IEnumerable<Product>>> 
        FindWithSellerAsync(Expression<Func<Product, bool>> expression)
    {
        try
        {
            var data = await _dbContext.Products
                .Include(p => p.Seller)
                .Where(expression)
                .ToListAsync();

            return Result<IEnumerable<Product>>.Success(data);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<Product>>
                .Failure(Error.InternalServerError(e.Message));
        }
    }
}