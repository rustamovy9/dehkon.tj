using Application.Contracts.IRepositories.IBaseRepository;
using Application.Extensions.ResultPattern;
using Domain.Entities;

namespace Application.Contracts.IRepositories;

public interface IMessageRepository : IGenericRepository<Message> 
{
    Task<Result<IEnumerable<Message>>> GetByChatIdAsync(int chatId);
    Task<Result<IEnumerable<Message>>> FindUnreadForUser(int chatId, int userId);
}