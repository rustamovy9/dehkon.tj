using Application.Extensions.Algorithms;
using Domain.Constants;
using Domain.Entities;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace MainApp.HelpersApi.Extensions.Seed;

public class Seeder(DataContext dbContext)
{
    public async Task InitialAsync()
    {
        await InitRolesAsync();
        await InitUsersAsync();
        await InitGlobalChatAsync();
        await InitMarketsAsync();
    }

    private async Task InitRolesAsync()
    {
        foreach (Role role in SeedData.Roles)
        {
            Role? existing = await dbContext.Roles
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(r => r.Id == role.Id);

            if (existing is null)
            {
                await dbContext.Roles.AddAsync(role);
            }
            else if (existing.IsDeleted)
            {
                existing.IsDeleted = false;
                existing.IsActive = true;
            }
        }

        await dbContext.SaveChangesAsync();
    }

    private async Task InitUsersAsync()
    {
        foreach (User user in SeedData.Users)
        {
            User? existing = await dbContext.Users
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            if (existing is null)
            {
                await dbContext.Users.AddAsync(user);
            }
            else if (existing.IsDeleted)
            {
                existing.IsDeleted = false;
                existing.IsActive = true;
            }
        }

        await dbContext.SaveChangesAsync();
    }

    private async Task InitGlobalChatAsync()
    {
        bool exist = await dbContext.Chats
            .IgnoreQueryFilters()
            .AnyAsync(c => c.IsGlobal);
        
        if(exist)
            return;

        Chat globalChat = new Chat()
        {
            IsGlobal = true,
            IsActive = true,
        };

        await dbContext.Chats.AddAsync(globalChat);
        await dbContext.SaveChangesAsync();
    }
    
    private async Task InitMarketsAsync()
    {
        foreach (Market market in SeedData.Markets)
        {
            Market? existing = await dbContext.Markets
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(m => m.Id == market.Id);

            if (existing is null)
            {
                await dbContext.Markets.AddAsync(market);
            }
            else if (existing.IsDeleted)
            {
                existing.IsDeleted = false;
                existing.IsActive = true;
            }
        }

        await dbContext.SaveChangesAsync();
    }
    
}



file static class SeedData
{
    public static readonly List<Role> Roles =
    [
        new Role
        {
            Id = 1,
            Name = DefaultRoles.Admin,
            Description = "System administrator"
        },
        new Role
        {
            Id = 2,
            Name = DefaultRoles.User,
            Description = "Default user"
        },
        new Role
        {
            Id = 3,
            Name = DefaultRoles.Seller,
            Description = "Product seller"
        },
        new Role
        {
            Id = 4,
            Name = DefaultRoles.Courier,
            Description = "Delivery courier"
        }
    ];

    public static readonly List<User> Users =
    [
        new User
        {
            Id = 1,
            UserName = "admin",
            FullName = "Admin Adminov",
            Email = "admin@gmail.com",
            PhoneNumber = "+992933448829",
            PasswordHash = HashAlgorithms.ConvertToHash("Admin123!"),
            RoleId = 1
        },
        new User
        {
            Id = 2,
            UserName = "user",
            FullName = "User Userov",
            Email = "user@gmail.com",
            PhoneNumber = "+992222222222",
            PasswordHash = HashAlgorithms.ConvertToHash("User123!"),
            RoleId = 2
        }
    ];
    
    public static readonly List<Market> Markets =
    [
        new Market
        {
            Id = 1,
            Name = "Мехргон",
            Slug = "mehrgon",
            Address = "г. Душанбе, рынок Мехргон",
            Latitude = 38.5733,
            Longitude = 68.7864
        },
        new Market
        {
            Id = 2,
            Name = "Саховат",
            Slug = "sakhovat",
            Address = "г. Душанбе, рынок Саховат",
            Latitude = 38.5615,
            Longitude = 68.7733
        },
        new Market
        {
            Id = 3,
            Name = "Корвон",
            Slug = "korvon",
            Address = "г. Душанбе, рынок Корвон"
        }
    ];
    
    
}
