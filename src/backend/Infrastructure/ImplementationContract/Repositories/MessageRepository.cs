using Application.Contracts.IRepositories;
using Application.Extensions.ResultPattern;
using Domain.Common;
using Domain.Entities;
using Infrastructure.DataAccess;
using Infrastructure.ImplementationContract.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ImplementationContract.Repositories;

public class MessageRepository(DataContext dbContext)
    : GenericRepository<Message>(dbContext), IMessageRepository
{
    private readonly DataContext _dbContext = dbContext;

    public async Task<Result<IEnumerable<Message>>> GetByChatIdAsync(int chatId)
    {
        try
        {
            var res =  await _dbContext.Messages
                .Include(m => m.Sender)
                .Where(m => m.ChatId == chatId)
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();
            return Result<IEnumerable<Message>>.Success(res);   
        }
        catch (Exception e)
        {
            return Result<IEnumerable<Message>>.Failure(Error.InternalServerError(e.Message));
        }
    }

    public async Task<Result<IEnumerable<Message>>> FindUnreadForUser(int chatId, int userId)
    {
        try
        {
            var messages = await _dbContext.Messages
                .Where(m =>
                    m.ChatId == chatId &&
                    m.SenderId != userId &&
                    !m.IsRead)
                .ToListAsync();

            return Result<IEnumerable<Message>>.Success(messages);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<Message>>.Failure(Error.InternalServerError(e.Message));
        }
    }
}