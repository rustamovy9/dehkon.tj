using Microsoft.AspNetCore.Http;

namespace Application.DTO_s;

public record UserReadInfo(
    int Id,
    string UserName,
    string Email,
    string FullName,
    string PhoneNumber,
    string ProfilePhotoUrl,
    string RoleName);
    
public record UserUpdateInfo(
    string UserName,
    string FullName,
    string? PhoneNumber,
    IFormFile? ProfilePhotoUrl);

