using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Filters;
using Application.Validation.Chat;
using Domain.Constants;
using MainApp.HelpersApi.Extensions.ResultPattern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MainApp.Controllers;

[ApiController]
[Route("api/chats")]
[Authorize]
public sealed class ChatController(IChatService service) : BaseController
{
    private int UserId =>
        int.Parse(User.FindFirst(CustomClaimTypes.Id)?.Value
                  ?? throw new UnauthorizedAccessException("UserId not found"));

    [HttpPost("private")]
    public async Task<IActionResult> CreatePrivateChat([FromBody] PrivateChatCreateInfo createInfo)
        => (await service.CreatePrivateChatAsync(UserId,createInfo)).ToActionResult();

    [HttpGet("my")]
    public async Task<IActionResult> GetMyChats()
        => (await service.GetMyChatsAsync(UserId)).ToActionResult();

    [Authorize(Roles = DefaultRoles.Admin)]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ChatFilter filter)
        => (await service.GetAllAsync(filter)).ToActionResult();
        
        
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetChatById(int id)
        => (await service.GetChatByIdAsync(id,UserId)).ToActionResult();

    
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => (await service.DeleteAsync(id,UserId)).ToActionResult();


}