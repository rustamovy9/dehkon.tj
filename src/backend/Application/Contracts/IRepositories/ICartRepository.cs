using Application.Extensions.ResultPattern;
using Domain.Entities;

namespace Application.Contracts.IRepositories;

public interface ICartRepository
{
    Task<Result<Cart>> GetByUserIdAsync(int userId);
}