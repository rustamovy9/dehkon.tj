using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Extensions.ResultPattern;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controllers;

[Route("/api/auth/")]
public sealed class AuthController(IAuthService authService) : BaseController
{
    private int UserId =>
        int.Parse(HttpContext.User.FindFirst(x => x.Type == CustomClaimTypes.Id)?.Value
                  ?? throw new UnauthorizedAccessException());

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        Result<LoginResult> result = await authService.LoginAsync(request);
        if (!result.IsSuccess)
            return BadRequest(result.Error.Message ?? "Login failed");

        return Ok(result.Value);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await authService.RegisterAsync(request);
        return result.IsSuccess ? Ok("User registered successfully.") : BadRequest(result.Error.Message ?? "User not registered.");
    }

    [Authorize]
    [HttpDelete("me")]
    public async Task<IActionResult> DeleteSelfAsync()
    {
        var result = await authService.DeleteAccountAsync(UserId);
        return result.IsSuccess ? Ok("Account deleted successfully.") : BadRequest(result.Error.Message ?? "Account not deleted.");
    }

    [Authorize(Roles = DefaultRoles.Admin)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUserAsync(int id)
    {
        var result = await authService.DeleteAccountAsync(id);
        return result.IsSuccess ? Ok("User deleted successfully.") : BadRequest(result.Error.Message ?? "User not deleted");
    }
    
    
    [Authorize]
    [HttpPut("change-password")]
    public async Task<IActionResult> ChangeOwnPassword(ChangePasswordRequest request)
    {
        var result = await authService.ChangePasswordAsync(UserId, request);
        return result.IsSuccess ? Ok("Password updated successfully.") : BadRequest(result.Error.Message ?? "Password not updated.");
    }

    [Authorize(Roles = DefaultRoles.Admin)]
    [HttpPut("{id:int}/change-password")]
    public async Task<IActionResult> ChangeUserPassword(int id, ChangePasswordRequest request)
    {
        var result = await authService.ChangePasswordAsync(id, request);
        return result.IsSuccess ? Ok("Password updated successfully") : BadRequest(result.Error.Message ?? "Password not updated");
    }
}