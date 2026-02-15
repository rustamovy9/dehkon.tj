using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Filters;
using Domain.Constants;
using MainApp.HelpersApi.Extensions.ResultPattern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controllers;

[ApiController]
[Route("api/markets")]
public sealed class MarketController(IMarketService service) : BaseController
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll([FromQuery] MarketFilter filter)
        => (await service.GetAllAsync(filter)).ToActionResult();


    // 📌 GET BY ID (доступен всем)
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
        => (await service.GetByIdAsync(id)).ToActionResult();


    // 📌 CREATE (только Admin)
    [HttpPost]
    [Authorize(Roles = DefaultRoles.Admin)]
    public async Task<IActionResult> Create([FromBody] MarketCreateInfo createInfo)
        => (await service.CreateAsync(createInfo)).ToActionResult();


    // 📌 UPDATE (только Admin)
    [HttpPut("{id:int}")]
    [Authorize(Roles = DefaultRoles.Admin)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] MarketUpdateInfo updateInfo)
        => (await service.UpdateAsync(id, updateInfo)).ToActionResult();


    // 📌 DELETE (только Admin)
    [HttpDelete("{id:int}")]
    [Authorize(Roles = DefaultRoles.Admin)]
    public async Task<IActionResult> Delete(int id)
        => (await service.DeleteAsync(id)).ToActionResult();
}