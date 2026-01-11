using System.Linq.Expressions;
using Application.Extensions.ResultPattern;
using Domain.Common;

namespace Application.Contracts.IRepositories.IBaseRepository.ICrud;

public interface IGenericFindRepository<T> where T : BaseEntity
{
    Task<Result<T?>> GetByIdAsync(int id);
    Task<Result<IEnumerable<T>>> GetAllAsync();
    Result<IQueryable<T>> Find(Expression<Func<T, bool>> expression);
}