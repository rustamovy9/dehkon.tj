using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Constants;
using Domain.Entities;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Extensions.Authentication;

public class AuthenticationService(IConfiguration config, DataContext dbContext) : IAuthenticationService
{
    public async Task<string> GenerateTokeAsync(User user)
    {
        string key = config["JWT':key"]!;

        SigningCredentials credentials =
            new SigningCredentials(GetSymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256);

        var roleName = await dbContext.Users
                                      .Where(u => u.Id == user.Id)
                                      .Select(u => u.Role.Name)
                                      .FirstAsync();
        
        List<Claim> claims =
        [
            new Claim(CustomClaimTypes.Id,user.Id.ToString()),
            new Claim(CustomClaimTypes.UserName,user.UserName),
            new Claim(CustomClaimTypes.Email,user.Email),
            new Claim(CustomClaimTypes.Phone,user.PhoneNumber),
            new Claim(CustomClaimTypes.FullName,user.FullName),
            new Claim(CustomClaimTypes.Role,roleName)
        ];

        
        JwtSecurityToken jwt = new JwtSecurityToken(
            issuer: config["JWT:issuer"],
            audience: config["JWT:audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public static SymmetricSecurityKey GetSymmetricSecurityKey(string key) =>
        new(Encoding.UTF8.GetBytes(key));
}