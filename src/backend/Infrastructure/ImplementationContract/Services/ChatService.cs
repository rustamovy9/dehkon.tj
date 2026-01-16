using Application.Contracts.IRepositories;
using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Extensions.ResultPattern;

namespace Infrastructure.ImplementationContract.Services;

public class ChatService(IChatRepository repository) : IChatService
{
    public Task<BaseResult> CreatePrivateChatAsync(int userId, int otherUserId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<ChatReadInfo>>> GetMyChatsAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<ChatReadInfo>> GetChatByIdAsync(int chatId, int userId)
    {
        throw new NotImplementedException();
    }
}