using Application.DTO_s;
using Application.Extensions.ResultPattern;

namespace Application.Contracts.IServices;

public interface IMessageService
{
    Task<BaseResult> SendMessageAsync(int userId, MessageCreateInfo createInfo);
    Task<Result<IEnumerable<MessageReadInfo>>> GetMessagesAsync(int chatId, int userId);
    Task<BaseResult> MarkAsReadAsync(int chatId, int userId);
}