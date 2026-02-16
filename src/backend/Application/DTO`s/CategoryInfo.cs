using Microsoft.AspNetCore.Http;

namespace Application.DTO_s;

public record CategoryReadInfo(
    int Id,
    string Name,
    string ImageUrl);

public record CategoryCreateInfo(
    string Name,
    IFormFile? ImageUrl);

public record CategoryUpdateInfo(
    string Name,
    IFormFile? ImageUrl);
    
