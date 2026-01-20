using Application.Contracts.IRepositories.IBaseRepository;
using Domain.Entities;

namespace Application.Contracts.IRepositories;

public interface IChatRepository : IGenericRepository<Chat>
{
    Task<Chat?> GetPrivateChatBetweenUsersAsync(int userId, int otherId);
}