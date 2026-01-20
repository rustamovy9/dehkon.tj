using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Filters;
using Domain.Constants;
using Domain.Enums;
using MainApp.HelpersApi.Extensions.ResultPattern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controllers;

[ApiController]
[Route("api/orders")]
[Authorize]
public sealed class OrderController(IOrderService service) : BaseController
{
    private int UserId =>
        int.Parse(User.FindFirst(CustomClaimTypes.Id)?.Value
                  ?? throw new UnauthorizedAccessException("UserId not found"));

    [HttpPost]
    [Authorize(Roles = DefaultRoles.User)]
    public async Task<IActionResult> CreateOrder([FromBody] OrderCreateInfo createInfo)
        => (await service.CreateOrderAsync(UserId, createInfo)).ToActionResult();

    [HttpGet("my")]
    [Authorize(Roles = DefaultRoles.User)]
    public async Task<IActionResult> GetMyOrders()
        => (await service.GetMyOrdersAsync(UserId)).ToActionResult();

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
        => (await service.GetOrderByIdAsync(id)).ToActionResult();

    [HttpPut("{id:int}/status")]
    [Authorize(Roles = DefaultRoles.Admin + "," + DefaultRoles.Courier)]
    public async Task<IActionResult> ChangeStatus(int id, [FromQuery] OrderStatus status)
        => (await service.ChangeStatusAsync(id, status)).ToActionResult();

    [Authorize(Roles = DefaultRoles.Admin)]
    [HttpGet]
    public async Task<IActionResult> GetOrders(OrderFilter filter)
        => (await service.GetOrdersAsync(filter)).ToActionResult();

}