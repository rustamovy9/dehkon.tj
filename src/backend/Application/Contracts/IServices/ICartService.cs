using Application.DTO_s;
using Application.Extensions.ResultPattern;

namespace Application.Contracts.IServices;

public interface ICartService
{
    Task<Result<CartReadInfo>> GetMyCartAsync(int userId);
    Task<BaseResult> AddItemAsync(int userId,CartItemCreateInfo createInfo);
    Task<BaseResult> UpdateItemAsync(int userId, CartItemUpdateInfo updateInfo);
    Task<BaseResult> RemoveItemAsync(int userId, int productId);
    Task<BaseResult> ClearCartAsync(int userId);
}