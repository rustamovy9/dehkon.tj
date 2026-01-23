using System.Linq.Expressions;
using Application.Contracts.IRepositories.IBaseRepository.ICrud;
using Application.Extensions.ResultPattern;
using Domain.Entities;

namespace Application.Contracts.IRepositories;

public interface IUserRepository : IGenericUpdateRepository<User>,IGenericFindRepository<User>
{
    Task<Result<User>> GetByIdWithRoleAsync(int id);
    Task<Result<IEnumerable<User>>> FindWithRole(Expression<Func<User, bool>> expression);
    Task<bool> ExistsAsync(Expression<Func<User, bool>> expression);
}