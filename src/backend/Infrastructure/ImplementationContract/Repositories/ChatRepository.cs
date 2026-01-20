using Application.Contracts.IRepositories;
using Domain.Entities;
using Infrastructure.DataAccess;
using Infrastructure.ImplementationContract.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ImplementationContract.Repositories;

public class ChatRepository(DataContext dbContext)
: GenericRepository<Chat>(dbContext),IChatRepository
{
    public async Task<Chat?> GetPrivateChatBetweenUsersAsync(int userId, int otherId)
    {
        return await dbContext.Chats
            .Include(c => c.ChatUsers)
            .Include(c => c.Messages)
            .Where(c => !c.IsGlobal)
            .FirstOrDefaultAsync(c =>
                c.ChatUsers.Any(x => x.UserId == userId) &&
                c.ChatUsers.Any(x => x.UserId == otherId));
    }
}