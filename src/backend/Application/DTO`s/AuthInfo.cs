using Domain.Entities;

namespace Application.DTO_s;

public class LoginRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public record LoginResult(
    string AccessToken);

public class RegisterRequest
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;

    public string Role { get; set; } = null!;
}

public class ChangePasswordRequest
{ 
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
}

public record AdminChangeUserRoleInfo(
    int RoleId
);

public record ForgotPasswordRequest(
    string Email
);

public record ResetPasswordRequest(
    string Email,
    string Code,
    string NewPassword,
    string ConfirmPassword
);