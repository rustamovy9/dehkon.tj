using Application.Contracts.IRepositories;
using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Extensions.ResultPattern;

namespace Infrastructure.ImplementationContract.Services;

public class CartService(ICartRepository repository) : ICartService
{
    public Task<Result<CartReadInfo>> GetMyCartAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult> AddItemAsync(int userId, CartItemCreateInfo createInfo)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult> UpdateItemAsync(int userId, CartItemUpdateInfo updateInfo)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult> RemoveItemAsync(int userId, int productId)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult> ClearCartAsync(int userId)
    {
        throw new NotImplementedException();
    }
}