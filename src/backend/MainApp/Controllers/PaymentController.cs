using Application.Contracts.IServices;
using Domain.Constants;
using MainApp.HelpersApi.Extensions.ResultPattern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controllers;

[ApiController]
[Route("api/payments")]
[Authorize]
public sealed class PaymentController(IPaymentService service) : BaseController
{
    private int UserId =>
        int.Parse(User.FindFirst(CustomClaimTypes.Id)?.Value
                  ?? throw new UnauthorizedAccessException("UserId not found"));

    [HttpPost("orders/{orderId:int}")]
    public async Task<IActionResult> PayOrder(int orderId)
        => (await service.PayOrderAsync(UserId, orderId)).ToActionResult();
}