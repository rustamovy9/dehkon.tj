using Application.Contracts.IServices;
using Application.DTO_s;
using Domain.Constants;
using MainApp.HelpersApi.Extensions.ResultPattern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controllers;

[ApiController]
[Route("api/messages")]
[Authorize]
public sealed class MessageController(IMessageService service) : BaseController
{
    private int UserId =>
        int.Parse(User.FindFirst(CustomClaimTypes.Id)?.Value
                  ?? throw new UnauthorizedAccessException("UserId not found"));

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] MessageCreateInfo createInfo)
        => (await service.SendMessageAsync(UserId, createInfo)).ToActionResult();

    [HttpGet("chat/{chatId:int}")]
    public async Task<IActionResult> GetMessages(int chatId)
        => (await service.GetMessagesAsync(chatId, UserId)).ToActionResult();

    [HttpPut("chat/{chatId:int}/read")]
    public async Task<IActionResult> MarkAsRead(int chatId)
        => (await service.MarkAsReadAsync(chatId, UserId)).ToActionResult();
}