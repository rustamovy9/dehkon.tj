using Application.Contracts.IRepositories;
using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Extensions.ResultPattern;

namespace Infrastructure.ImplementationContract.Services;

public class MessageService(IMessageRepository repository) : IMessageService
{
    public Task<BaseResult> SendMessageAsync(int userId, MessageCreateInfo createInfo)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<MessageReadInfo>>> GetMessagesAsync(int chatId, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult> MarkAsReadAsync(int chatId, int userId)
    {
        throw new NotImplementedException();
    }
}