using System.Linq.Expressions;
using Application.Contracts.IRepositories;
using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Extensions.Mappers;
using Application.Extensions.Responses.PagedResponse;
using Application.Extensions.ResultPattern;
using Application.Filters;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Extensions;

namespace Infrastructure.ImplementationContract.Services;

public class ChatService(IChatRepository chatRepository,IUserRepository userRepository) : IChatService
{
    public async Task<Result<ChatReadInfo>> CreatePrivateChatAsync(int userId, PrivateChatCreateInfo createInfo)
    {
        if (userId == createInfo.OtherUserId)
            return Result<ChatReadInfo>.Failure(
                Error.BadRequest("You cannot create chat with yourself"));

        var otherUser = await userRepository.GetByIdAsync(createInfo.OtherUserId);
        if (!otherUser.IsSuccess)
            return Result<ChatReadInfo>.Failure(Error.NotFound("User not found"));

        Result<Chat?> existingChat =
            await chatRepository.GetPrivateChatBetweenUsersAsync(
                userId, createInfo.OtherUserId);

        if (existingChat.Value != null)
            return Result<ChatReadInfo>.Success(existingChat.Value.ToRead());

        Chat chat = ChatMap.ToPrivateChat(userId, createInfo);

        var result = await chatRepository.AddAsync(chat);
        if (!result.IsSuccess)
            return Result<ChatReadInfo>.Failure(result.Error);

        var chatFromDb = await chatRepository.GetByIdWithUsersAsync(chat.Id);
        
        return result.IsSuccess
            ? Result<ChatReadInfo>.Success(chatFromDb.Value!.ToRead())
            : Result<ChatReadInfo>.Failure(chatFromDb.Error);
    }

    public async Task<Result<IEnumerable<ChatReadInfo>>> GetMyChatsAsync(int userId)
    {
        Result<IEnumerable<Chat>> res = await chatRepository.Find(c => c.ChatUsers.Any(cu => cu.UserId == userId));

        if (!res.IsSuccess)
            return Result<IEnumerable<ChatReadInfo>>.Failure(res.Error);
      
        return  Result<IEnumerable<ChatReadInfo>>.Success(res.Value!.Select(c => c.ToRead()).ToList());
         
    }

    public async Task<Result<PagedResponse<IEnumerable<ChatReadInfo>>>> GetAllAsync(ChatFilter filter)
    {
        Expression<Func<Chat, bool>> filterExpression = ch =>
            (filter.IsGlobal == null || ch.IsGlobal == filter.IsGlobal) &&
            (filter.UserId == null || ch.ChatUsers.Any(cu => cu.UserId == filter.UserId));

        var req = await chatRepository.Find(filterExpression);

        if (!req.IsSuccess)
            return Result<PagedResponse<IEnumerable<ChatReadInfo>>>.Failure(req.Error);

        List<ChatReadInfo> query = req.Value!
            .OrderByDescending(c=>c.CreatedAt)
            .Select(c=>c.ToRead())
            .ToList();

        int count = query.Count;

        IEnumerable<ChatReadInfo> chats = query.Page(filter.PageNumber, filter.PageSize);

        var response = PagedResponse<IEnumerable<ChatReadInfo>>
            .Create(filter.PageNumber,filter.PageSize,count,chats);

        return Result<PagedResponse<IEnumerable<ChatReadInfo>>>.Success(response);
    }

    public async Task<Result<ChatReadInfo>> GetChatByIdAsync(int chatId, int userId)
    {
        Result<Chat> chatRes = await chatRepository.GetByIdAsync(chatId);
        if (!chatRes.IsSuccess)
            return Result<ChatReadInfo>.Failure(chatRes.Error);

        Chat chat = chatRes.Value!;

        bool hasAccess = chat.ChatUsers.Any(cu => cu.UserId == userId);
        if (!hasAccess)
            return Result<ChatReadInfo>.Failure(Error.Forbidden());

        return Result<ChatReadInfo>.Success(chat.ToRead());
    }

    public async Task<BaseResult> DeleteAsync(int chatId, int userId)
    {
        Result<Chat> chatRes = await chatRepository.GetByIdAsync(chatId);
        if(!chatRes.IsSuccess)
            return BaseResult.Failure(chatRes.Error);

        Chat chat = chatRes.Value!;

        ChatUser? chatUser = chat.ChatUsers.FirstOrDefault(cu => cu.UserId == userId);
        
        if(chatUser is null)
            return BaseResult.Failure(Error.Forbidden());

        chat.ChatUsers.Remove(chatUser);

        if (!chat.IsGlobal && !chat.ChatUsers.Any())
        {
            BaseResult deleteResult = await chatRepository.DeleteAsync(chat.Id);
            return deleteResult.IsSuccess
                ? BaseResult.Success()
                : BaseResult.Failure(deleteResult.Error);
        }

        BaseResult updateRes = await chatRepository.UpdateAsync(chat);
        return updateRes.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(updateRes.Error);
    }

    public async Task<Result<ChatReadInfo>> GetGlobalChatAsync()
    {
        var chatRes = await chatRepository.GetGlobalChatAsync();


        return chatRes.IsSuccess
            ? Result<ChatReadInfo>.Success(chatRes.Value!.ToRead())
            : Result<ChatReadInfo>.Failure(chatRes.Error);
    }
}