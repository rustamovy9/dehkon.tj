using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Filters;
using Domain.Constants;
using MainApp.HelpersApi.Extensions.ResultPattern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controllers;

[ApiController]
[Route("api/announcements")]
public sealed class AnnouncementController(IAnnouncementService service) : BaseController
{
    private int UserId =>
        int.Parse(User.FindFirst(CustomClaimTypes.Id)?.Value
                  ?? throw new UnauthorizedAccessException("UserId not found"));

    [Authorize(Roles = DefaultRoles.Admin)]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AnnouncementCreateInfo createInfo)
        => (await service.CreateAsync(UserId,createInfo)).ToActionResult();

    [Authorize(Roles = DefaultRoles.Admin)]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] AnnouncementUpdateInfo updateInfo)
        => (await service.UpdateAsync(id,updateInfo)).ToActionResult();

    [Authorize(Roles = DefaultRoles.Admin)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => (await service.DeleteAsync(id)).ToActionResult();

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] AnnouncementFilter filter)
        => (await service.GetAllAsync(filter)).ToActionResult();
}