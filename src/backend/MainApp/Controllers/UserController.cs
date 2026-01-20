using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Filters;
using Domain.Constants;
using MainApp.HelpersApi.Extensions.ResultPattern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controllers;

[ApiController]
[Route("api/users")]
public sealed class UserController(IUserService service) : BaseController
{
    private int UserId =>
        int.Parse(User.FindFirst(CustomClaimTypes.Id)?.Value
                  ?? throw new UnauthorizedAccessException("UserId not found"));

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMyProfileAsync()
        => (await service.GetMyProfileAsync(UserId)).ToActionResult();

    [Authorize]
    [HttpPut("me")]
    public async Task<IActionResult> UpdateMyProfile([FromForm] UserUpdateInfo updateInfo)
        => (await service.UpdateProfileAsync(UserId, updateInfo)).ToActionResult();


    [Authorize(Roles = DefaultRoles.Admin)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserById(int id)
        => (await service.GetUserByIdAsync(id)).ToActionResult();


    [Authorize(Roles = DefaultRoles.Admin)]
    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] UserFilter filter)
        => (await service.GetUsersAsync(filter)).ToActionResult();
}