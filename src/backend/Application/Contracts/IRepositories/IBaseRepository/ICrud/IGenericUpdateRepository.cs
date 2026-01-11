using Application.Extensions.ResultPattern;
using Domain.Common;

namespace Application.Contracts.IRepositories.IBaseRepository.ICrud;

public interface IGenericUpdateRepository<T> where T : BaseEntity
{
    Task<Result<int>> UpdateAsync(T value);
}