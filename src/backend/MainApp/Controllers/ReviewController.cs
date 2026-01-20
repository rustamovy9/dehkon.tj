using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Filters;
using Domain.Constants;
using MainApp.HelpersApi.Extensions.ResultPattern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controllers;

[ApiController]
[Route("api/reviews")]
public sealed class ReviewController(IReviewService service) : BaseController
{
    private int UserId =>
        int.Parse(User.FindFirst(CustomClaimTypes.Id)?.Value!);

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ReviewCreateInfo createInfo)
        => (await service.CreatAsync(UserId, createInfo)).ToActionResult();

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ReviewUpdateInfo updateInfo)
        => (await service.UpdateAsync(id, UserId, updateInfo)).ToActionResult();

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => (await service.DeleteAsync(id, UserId)).ToActionResult();

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ReviewFilter filter)
        => (await service.GetAllAsync(filter)).ToActionResult();

    [AllowAnonymous]
    [HttpGet("product/{productId:int}")]
    public async Task<IActionResult> GetByProduct(int productId)
        => (await service.GetByProductAsync(productId)).ToActionResult();
}