using Microsoft.AspNetCore.Http;

namespace Application.DTO_s;

public record CategoryReadInfo(
    int Id,
    string Name);

public record CategoryCreateInfo(
    IFormFile? ImageUrl,
    string Name);

public record CategoryUpdateInfo(
    IFormFile? ImageUrl,
    string Name);
    
