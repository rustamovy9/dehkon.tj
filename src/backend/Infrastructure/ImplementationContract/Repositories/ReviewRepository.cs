using System.Linq.Expressions;
using Application.Contracts.IRepositories;
using Application.Extensions.ResultPattern;
using Domain.Common;
using Domain.Entities;
using Infrastructure.DataAccess;
using Infrastructure.ImplementationContract.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ImplementationContract.Repositories;

public class ReviewRepository(DataContext dbContext)
    : GenericRepository<Review>(dbContext), IReviewRepository
{
    private readonly DataContext _dbContext = dbContext;

    public async Task<Result<IEnumerable<Review>>> FindWithUserAsync(
        Expression<Func<Review, bool>> expression)
    {
        try
        {
            var data = await _dbContext.Reviews
                .Include(r => r.User)
                .Include(r => r.Product) 
                .Where(expression)
                .ToListAsync();

            return Result<IEnumerable<Review>>.Success(data);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<Review>>
                .Failure(Error.InternalServerError(e.Message));
        }
    }
}