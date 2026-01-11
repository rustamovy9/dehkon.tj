using Application.Extensions.ResultPattern;
using Domain.Common;

namespace Application.Contracts.IRepositories.IBaseRepository.ICrud;

public interface IGenericDeleteRepository<T> where T : BaseEntity
{
    Task<Result<int>> DeleteAsync(int id);
    Task<Result<int>> DeleteAsync(T value);
}