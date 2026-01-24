using System.Collections;
using System.Linq.Expressions;
using Application.Contracts.IRepositories;
using Application.Extensions.ResultPattern;
using Domain.Common;
using Domain.Entities;
using Infrastructure.DataAccess;
using Infrastructure.ImplementationContract.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ImplementationContract.Repositories;

public class ChatRepository(DataContext dbContext)
    : GenericRepository<Chat>(dbContext), IChatRepository
{
    private readonly DataContext _dbContext = dbContext;

    public async Task<Result<Chat?>> GetPrivateChatBetweenUsersAsync(int userId, int otherId)
    {
        try
        {
            var res = await _dbContext.Chats
                .Include(c => c.ChatUsers)
                .ThenInclude(c => c.User)
                .Include(c => c.Messages)
                .Where(c => !c.IsGlobal)
                .FirstOrDefaultAsync(c =>
                    c.ChatUsers.Any(x => x.UserId == userId) &&
                    c.ChatUsers.Any(x => x.UserId == otherId));

            return Result<Chat?>.Success(res);
        }
        catch (Exception e)
        {
            return Result<Chat?>.Failure(Error.InternalServerError(e.Message));
        }
    }

    public async Task<Result<Chat>> GetByIdWithUsersAsync(int id)
    {
        try
        {
            var chat = await _dbContext.Chats
                .Include(c => c.ChatUsers)
                .ThenInclude(cu => cu.User)
                .Include(cu => cu.Messages)
                .ThenInclude(m => m.Sender)
                .FirstOrDefaultAsync(c => c.Id == id);

            return chat != null
                ? Result<Chat>.Success(chat)
                : Result<Chat>.Failure(Error.NotFound());
        }
        catch (Exception e)
        {
            return Result<Chat>.Failure(Error.InternalServerError(e.Message));
        }
    }

    public async Task<Result<Chat>> GetGlobalChatAsync()
    {
        try
        {
            var chat = await _dbContext.Chats
                .Include(c => c.ChatUsers)
                .ThenInclude(u => u.User)
                .Include(cu => cu.Messages)
                .ThenInclude(m => m.Sender)
                .FirstOrDefaultAsync(c => c.IsGlobal);

            return chat != null
                ? Result<Chat>.Success(chat)
                : Result<Chat>.Failure(Error.NotFound("Global chat not found"));
        }
        catch (Exception e)
        {
            return Result<Chat>.Failure(Error.InternalServerError(e.Message));
        }
    }

    public async Task<Result<IEnumerable<Chat>>> FindWithDetailsAsync(Expression<Func<Chat, bool>> expression)
    {
        try
        {
            var data = await _dbContext.Chats
                .Include(c => c.ChatUsers)
                .ThenInclude(cu => cu.User)
                .Include(m => m.Messages)
                .ThenInclude(m => m.Sender)
                .Where(expression)
                .ToListAsync();

            return Result<IEnumerable<Chat>>.Success(data);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<Chat>>.Failure(Error.InternalServerError(e.Message));
        }
    }
    
    
    public async Task<BaseResult> RemoveUserAsync(int chatId, int userId)
    {
        try
        {
            var chatUser = await _dbContext.ChatUsers
                .FirstOrDefaultAsync(cu =>
                    cu.ChatId == chatId && cu.UserId == userId);

            if (chatUser != null)
            {
                _dbContext.ChatUsers.Remove(chatUser);
                await _dbContext.SaveChangesAsync();
                return BaseResult.Success();
            }
            return BaseResult.Failure(Error.NotFound());
        }
        catch (Exception e)
        {
            return BaseResult.Failure(Error.InternalServerError(e.Message));
        }
    }
}