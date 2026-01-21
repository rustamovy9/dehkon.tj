using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Extensions.Algorithms;
using Application.Extensions.Mappers;
using Application.Extensions.ResultPattern;
using Domain.Common;
using Domain.Constants;
using Domain.Entities;
using Infrastructure.DataAccess;
using Infrastructure.Extensions.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ImplementationContract.Services;

public class AuthService(DataContext dbContext,IAuthenticationService service) : IAuthService
{
    public async Task<Result<LoginResult>> LoginAsync(LoginRequest request)
    {
        User? user = await dbContext.Users
            .Include(u=>u.Role)
            .FirstOrDefaultAsync(x => (x.Email == request.Email || (x.UserName == request.Email))
                                       && x.PasswordHash == HashAlgorithms.ConvertToHash(request.Password));

        if (user is null)
            return Result<LoginResult>.Failure(Error.BadRequest("Invalid username or password"));
        

        string token = await service.GenerateTokeAsync(user);
        
        return Result<LoginResult>.Success(new LoginResult(token));
    }

    public async Task<BaseResult> RegisterAsync(RegisterRequest request)
    {
        bool conflict = await dbContext.Users.AnyAsync(u=>
            u.UserName == request.UserName || 
            u.Email == request.Email ||
            u.PhoneNumber == request.PhoneNumber);
        
        if(conflict)
            return BaseResult.Failure(Error.Conflict("User already exists"));

        string roleName = request.Role;

        if (roleName == DefaultRoles.Admin) 
            return BaseResult.Failure(Error.BadRequest("You cannot register as Admin"));

        if (roleName != DefaultRoles.User &&
            roleName != DefaultRoles.Seller &&
            roleName != DefaultRoles.Courier)
            return BaseResult.Failure(Error.BadRequest("Invalid role"));

        var role = await dbContext.Roles
            .FirstOrDefaultAsync(r=>r.Name == roleName);
        
        if(role == null)
            return BaseResult.Failure(Error.NotFound("Role not found"));

        User user = request.ToEntity();
        user.RoleId = role.Id;

        user.Cart = new Cart();
        
        await dbContext.AddAsync(user);
        await dbContext.SaveChangesAsync();

        return BaseResult.Success();
    }

    public async Task<BaseResult> DeleteAccountAsync(int id)
    {
        User? user = await dbContext.Users.FirstOrDefaultAsync(i => i.Id == id);
        if (user is null) return BaseResult.Failure(Error.NotFound());

        user.ToDelete();
        await dbContext.SaveChangesAsync();
        return BaseResult.Success();
    }

    public async Task<BaseResult> ChangePasswordAsync(int userId, ChangePasswordRequest request)
    {
        User? user = await dbContext.Users.FirstOrDefaultAsync(i => i.Id == userId);
        if(user is null)return BaseResult.Failure(Error.NotFound());

        bool checkPassword = user.PasswordHash == HashAlgorithms.ConvertToHash(request.OldPassword);
        if (!checkPassword) return BaseResult.Failure(Error.BadRequest("Password is incorrect"));

        user.PasswordHash = HashAlgorithms.ConvertToHash(request.NewPassword);

        await dbContext.SaveChangesAsync();
        return BaseResult.Success();
    }
}