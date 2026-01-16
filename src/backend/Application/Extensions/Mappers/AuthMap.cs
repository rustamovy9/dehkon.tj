using Application.DTO_s;
using Application.Extensions.Algorithms;
using Domain.Entities;

namespace Application.Extensions.Mappers;

public static class AuthMap
{
    public static User ToEntity(this RegisterRequest request) =>new User()
        {
            UserName = request.UserName,
            FullName = request.FullName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            PasswordHash = HashAlgorithms.ConvertToHash(request.Password),
        };
    
    public static User ToDelete(this User user)
    {
        user.IsDeleted = true;
        user.IsActive = false;
        user.DeletedAt = DateTimeOffset.UtcNow;
        user.Version++;
        return user;
    }
}