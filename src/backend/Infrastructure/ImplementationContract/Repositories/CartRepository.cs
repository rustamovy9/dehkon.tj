using Application.Contracts.IRepositories;
using Application.Extensions.ResultPattern;
using Domain.Common;
using Domain.Entities;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ImplementationContract.Repositories;

public class CartRepository(DataContext dbContext) : ICartRepository
{
    public async Task<Result<Cart>> GetByUserIdAsync(int userId)
    {
        try
        {
            Cart? cart = await dbContext.Set<Cart>()
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(ui => ui.UserId == userId);

            return cart != null
                ? Result<Cart>.Success(cart)
                : Result<Cart>.Failure(Error.NotFound());
        }
        catch (Exception e)
        {
            return Result<Cart>.Failure(Error.InternalServerError(e.Message));
        }
    }
}