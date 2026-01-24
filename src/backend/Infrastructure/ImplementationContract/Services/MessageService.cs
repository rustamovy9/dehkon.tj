using Application.Contracts.IRepositories;
using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Extensions.Mappers;
using Application.Extensions.ResultPattern;
using Domain.Common;
using Domain.Entities;

namespace Infrastructure.ImplementationContract.Services;

public class MessageService(IMessageRepository messageRepository, IChatRepository chatRepository) : IMessageService
{
    public async Task<BaseResult> SendMessageAsync(int userId, MessageCreateInfo createInfo)
    {
        var chatRes = await chatRepository.GetByIdWithUsersAsync(createInfo.ChatId);
        if (!chatRes.IsSuccess)
            return BaseResult.Failure(chatRes.Error);

        Chat chat = chatRes.Value!;

        bool isParticipant = chat.IsGlobal ||
                             chat.ChatUsers.Any(cu => cu.UserId == userId);
        if (!isParticipant)
            return BaseResult.Failure(Error.Forbidden("You are not a member of this chat"));

        Message message = createInfo.ToEntity(userId);

        var res = await messageRepository.AddAsync(message);

        return res.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(res.Error);
    }

    public async Task<Result<IEnumerable<MessageReadInfo>>> GetMessagesAsync(int chatId, int userId)
    {
        var chatRes = await chatRepository.GetByIdWithUsersAsync(chatId);
        if (!chatRes.IsSuccess)
            return Result<IEnumerable<MessageReadInfo>>.Failure(chatRes.Error);

        Chat chat = chatRes.Value!;

        bool isParticipant = chat.IsGlobal ||
                             chat.ChatUsers.Any(cu => cu.UserId == userId);

        if (!isParticipant)
            return Result<IEnumerable<MessageReadInfo>>.Failure(Error.Forbidden());

        var request = await messageRepository.GetByChatIdAsync(chatId);

        if (!request.IsSuccess)
            return Result<IEnumerable<MessageReadInfo>>.Failure(request.Error);

        IEnumerable<MessageReadInfo> messages = request.Value!
            .OrderBy(m => m.CreatedAt)
            .Select(m => m.ToRead());

        return Result<IEnumerable<MessageReadInfo>>.Success(messages);
    }

    public async Task<BaseResult> MarkAsReadAsync(int chatId, int userId)
    {
        var chatRes = await chatRepository.GetByIdWithUsersAsync(chatId);
        if (!chatRes.IsSuccess)
            return BaseResult.Failure(chatRes.Error);

        Chat chat = chatRes.Value!;

        bool isParticipant = chat.IsGlobal ||
                             chat.ChatUsers.Any(cu => cu.UserId == userId);

        if (!isParticipant)
            return BaseResult.Failure(Error.Forbidden());

        var request = await messageRepository.FindUnreadForUser(chatId, userId);

        if (!request.IsSuccess)
            return BaseResult.Failure(request.Error);

        foreach (var message in request.Value!)
        {
            message.IsRead = true;
            await messageRepository.UpdateAsync(message);
        }


        return BaseResult.Success();
    }
}