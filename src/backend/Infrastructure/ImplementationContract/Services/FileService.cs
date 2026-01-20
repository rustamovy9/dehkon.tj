using Application.Contracts.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.ImplementationContract.Services;

public class FileService : IFileService
{
    private const long MaxFileSize = 50 * 1024 * 1024;
    private readonly string _rootPath;

    private readonly HashSet<string> _allowedExtensions = new(StringComparer.OrdinalIgnoreCase)
        { ".jpg", ".png",".jpeg",".mp4" };
    
    public FileService(IWebHostEnvironment env)
    {
        _rootPath = env.WebRootPath
                    ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        if (!Directory.Exists(_rootPath))
            Directory.CreateDirectory(_rootPath);
    }

    public async Task<string> CreateFile(IFormFile file, string folder)
    {
        if (!_allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower())) 
            throw new InvalidOperationException("Invalid file type.");

        if (file.Length > MaxFileSize)
            throw new InvalidOperationException("File size exceeds the maximum allowed size.");

        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}"; 
        string folderPath = Path.Combine(_rootPath, folder); 

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string fullPath = Path.Combine(folderPath, fileName);

        try
        {
            await using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
        catch (Exception ex)
        {
            await Console.Error.WriteLineAsync(ex.Message);
            throw new InvalidOperationException("An error occurred while saving the file.");
        }
    }

    public bool DeleteFile(string? file, string folder)
    {
        if (string.IsNullOrWhiteSpace(file))
            return false;

        string fullPath = Path.Combine(_rootPath, folder, file);

        if (!File.Exists(fullPath))
            return false;

        File.Delete(fullPath);
        return true;
    }
}