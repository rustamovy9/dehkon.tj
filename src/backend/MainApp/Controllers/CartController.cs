using Application.Contracts.IServices;
using Application.DTO_s;
using Domain.Constants;
using Domain.Entities;
using MainApp.HelpersApi.Extensions.ResultPattern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controllers;

[ApiController]
[Route("api/cart")]
[Authorize]
public sealed class CartController(ICartService service) : BaseController
{
    private int UserId =>
        int.Parse(User.FindFirst(CustomClaimTypes.Id)?.Value
                  ?? throw new UnauthorizedAccessException("UserId not found"));

    [HttpGet]
    public async Task<IActionResult> GetMyCart()
        => (await service.GetMyCartAsync(UserId)).ToActionResult();

    [HttpPost("items")]
    public async Task<IActionResult> AddItem([FromBody] CartItemCreateInfo createInfo)
        => (await service.AddItemAsync(UserId, createInfo)).ToActionResult();

    [HttpPut("items/{itemId:int}")]
    public async Task<IActionResult> UpdateItem(int itemId, [FromBody] CartItemUpdateInfo updateInfo)
        => (await service.UpdateItemAsync(UserId, itemId, updateInfo)).ToActionResult();

    [HttpDelete("items/{itemId:int}")]
    public async Task<IActionResult> RemoveItem(int itemId)
        => (await service.RemoveItemAsync(UserId, itemId)).ToActionResult();

    [HttpDelete("clear")]
    public async Task<IActionResult> Clear()
        => (await service.ClearCartAsync(UserId)).ToActionResult();
}