using System.Collections;
using System.Linq.Expressions;
using Application.Contracts.IRepositories.IBaseRepository;
using Application.Extensions.ResultPattern;
using Domain.Common;
using Domain.Extensions;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Exception = System.Exception;

namespace Infrastructure.ImplementationContract.Repositories.BaseRepository;

public class GenericRepository<T>(DataContext dbContext) : IGenericRepository<T> where T : BaseEntity
{
    public async Task<Result<int>> AddAsync(T entity)
    {
        try
        {
            await dbContext.Set<T>().AddAsync(entity);
            int res = await dbContext.SaveChangesAsync();

            return res > 0
                ? Result<int>.Success(res)
                : Result<int>.Failure(Error.InternalServerError());
        }
        catch (Exception e)
        {
            return Result<int>.Failure(Error.InternalServerError(e.Message));
        }
    }

    public async Task<Result<int>> AddRangeAsync(IEnumerable<T> entities)
    {
        try
        {
            await dbContext.Set<T>().AddRangeAsync(entities);
            int res = await dbContext.SaveChangesAsync();
            return res > 0
                ? Result<int>.Success(res)
                : Result<int>.Failure(Error.InternalServerError());
        }
        catch (Exception e)
        {
            return Result<int>.Failure(Error.InternalServerError(e.Message));
        }
    }

    public async Task<Result<int>> UpdateAsync(T value)
    {
        try
        {
            T? existing = await dbContext.Set<T>().AsTracking().FirstOrDefaultAsync(x => x.Id == value.Id && !x.IsDeleted);
            if (existing == null)
                return Result<int>.Failure(Error.NotFound());
            
            dbContext.Entry(existing).CurrentValues.SetValues(value);

            if (dbContext.Entry(existing).State == EntityState.Unchanged)
                return Result<int>.Success(1);

            int res = await dbContext.SaveChangesAsync();
            return res > 0
                ? Result<int>.Success(res)
                : Result<int>.Failure(Error.InternalServerError());
        }
        catch (Exception e)
        {
            return Result<int>.Failure(Error.InternalServerError(e.Message));
        }
    }

    public async Task<Result<int>> DeleteAsync(int id)
    {
        try
        {
            T? entity = await dbContext.Set<T>().AsTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                return Result<int>.Failure(Error.NotFound());

            entity.ToDelete();

            if (dbContext.Entry(entity).State == EntityState.Unchanged)
                return Result<int>.Success(1);

            int res = await dbContext.SaveChangesAsync();
            return res > 0
                ? Result<int>.Success(res)
                : Result<int>.Failure(Error.InternalServerError());
        }
        catch (Exception e)
        {
            return Result<int>.Failure(Error.InternalServerError(e.Message));
        }
    }

    public async Task<Result<int>> DeleteAsync(T value)
    {
        try
        {
            T? entity = await dbContext.Set<T>().AsTracking().FirstOrDefaultAsync(x => x.Id == value.Id);
            if (entity == null) 
                return Result<int>.Failure(Error.NotFound());

            entity.ToDelete();

            if (dbContext.Entry(entity).State == EntityState.Unchanged)
                return Result<int>.Success(1);
            
            int res = await dbContext.SaveChangesAsync();
            return res > 0
                ? Result<int>.Success(res)
                : Result<int>.Failure(Error.InternalServerError());
        }
        catch (Exception e)
        {
            return Result<int>.Failure(Error.InternalServerError(e.Message));
        }
    }

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
           List<T> data =  await dbContext.Set<T>()
                    .Where(expression).ToListAsync();

           return Result<IEnumerable<T>>.Success(data);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<T>>.Failure(Error.InternalServerError(e.Message));
        }
    }
}