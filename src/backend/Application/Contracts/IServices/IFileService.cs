using Microsoft.AspNetCore.Http;

namespace Application.Contracts.IServices;

public interface IFileService
{
    Task<string> CreateFile(IFormFile file,string folder);
    bool DeleteFile(string file,string folder);
}