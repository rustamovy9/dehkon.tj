using Application.DTO_s;
using Application.Extensions.ResultPattern;

namespace Application.Contracts.IServices;

public interface IChatService
{
    Task<BaseResult> CreatePrivateChatAsync(int userId,int otherUserId);
    Task<Result<IEnumerable<ChatReadInfo>>> GetMyChatsAsync(int userId);
    Task<Result<ChatReadInfo>> GetChatByIdAsync(int chatId, int userId);
}