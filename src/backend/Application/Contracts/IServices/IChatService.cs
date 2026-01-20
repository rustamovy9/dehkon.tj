using Application.DTO_s;
using Application.Extensions.Responses.PagedResponse;
using Application.Extensions.ResultPattern;
using Application.Filters;

namespace Application.Contracts.IServices;

public interface IChatService
{
    Task<Result<ChatReadInfo>> CreatePrivateChatAsync(int userId,PrivateChatCreateInfo privateChatCreateInfo);
    Task<Result<IEnumerable<ChatReadInfo>>> GetMyChatsAsync(int userId);
    Task<Result<PagedResponse<IEnumerable<ChatReadInfo>>>> GetAllAsync(ChatFilter filter);
    Task<Result<ChatReadInfo>> GetChatByIdAsync(int chatId, int userId);
    Task<BaseResult> DeleteAsync(int chatId, int userId);
}