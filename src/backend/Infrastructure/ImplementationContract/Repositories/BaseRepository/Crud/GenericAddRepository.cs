using Application.Contracts.IRepositories.IBaseRepository.ICrud;
using Application.Extensions.ResultPattern;
using Domain.Common;
using Infrastructure.DataAccess;

namespace Infrastructure.ImplementationContract.Repositories.BaseRepository.Crud;

public class GenericAddRepository<T>(DataContext dbContext) : IGenericAddRepository<T> where T : BaseEntity
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
}