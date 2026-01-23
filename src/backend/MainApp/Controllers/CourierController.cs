using Application.Contracts.IServices;
using Domain.Constants;
using Domain.Entities;
using MainApp.HelpersApi.Extensions.ResultPattern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controllers;

[ApiController]
[Route("api/couriers")]
[Authorize(Roles = DefaultRoles.Admin + "," + DefaultRoles.Courier)]
public class CourierController(ICourierService service) : BaseController
{
    private int UserId =>
        int.Parse(User.FindFirst(CustomClaimTypes.Id)!.Value);

    [HttpGet("orders/available")]   
    public async Task<IActionResult> GetAvailable()
        => (await service.GetAvailableOrdersAsync()).ToActionResult();

    [HttpPost("orders/{orderId:int}/take")]
    public async Task<IActionResult> Take(int orderId)
        => (await service.TakeOrderAsync(UserId, orderId)).ToActionResult();

    [HttpPost("orders/{orderId:int}/deliver")]
    public async Task<IActionResult> Deliver(int orderId)
        => (await service.DeliverOrderAsync(UserId, orderId)).ToActionResult();

    [HttpGet("orders/my")]
    public async Task<IActionResult> MyOrders()
        => (await service.GetMyOrdersAsync(UserId)).ToActionResult();

}