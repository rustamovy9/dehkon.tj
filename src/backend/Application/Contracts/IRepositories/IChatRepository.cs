using Application.Contracts.IRepositories.IBaseRepository;
using Application.Extensions.ResultPattern;
using Domain.Entities;

namespace Application.Contracts.IRepositories;

public interface IChatRepository : IGenericRepository<Chat>
{
    Task<Result<Chat?>> GetPrivateChatBetweenUsersAsync(int userId, int otherId);
    Task<Result<Chat>> GetByIdWithUsersAsync(int id);
    Task<Result<Chat>> GetGlobalChatAsync();
}