using System.Linq.Expressions;
using Application.Contracts.IRepositories.IBaseRepository.ICrud;
using Application.Extensions.ResultPattern;
using Domain.Common;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ImplementationContract.Repositories.BaseRepository.Crud;

public class GenericFindRepository<T>(DataContext dbContext) : IGenericFindRepository<T> where T : BaseEntity
{
    public async Task<Result<T>> GetByIdAsync(int id)
    {
        try
        {
            T? res = await dbContext.Set<T>()
                .FirstOrDefaultAsync(x => x.Id == id);
            return res != null
                ? Result<T>.Success(res)
                : Result<T>.Failure(Error.NotFound());
        }
        catch (Exception e)
        {
            return Result<T>.Failure(Error.InternalServerError(e.Message));
        }
    }

    public async Task<Result<IEnumerable<T>>> GetAllAsync()
    {
        try
        {
            return Result<IEnumerable<T>>
                .Success(await dbContext.Set<T>()
                    .ToListAsync());
        }
        catch (Exception e)
        {
            return Result<IEnumerable<T>>.Failure(Error.InternalServerError(e.Message));
        }
    }

    public async Task<Result<IEnumerable<T>>> Find(Expression<Func<T, bool>> expression)
    {
        try
        {
            List<T> data = await dbContext.Set<T>()
                    .Where(expression).ToListAsync();

            return Result<IEnumerable<T>>.Success(data);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<T>>.Failure(Error.InternalServerError(e.Message));
        }
    }
}