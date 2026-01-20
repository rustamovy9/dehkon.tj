using Application.Contracts.IRepositories;
using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Extensions.Mappers;
using Application.Extensions.ResultPattern;
using Domain.Common;
using Domain.Entities;

namespace Infrastructure.ImplementationContract.Services;

public class CartService(ICartRepository cartRepository,ICartItemRepository cartItemRepository,IProductRepository productRepository) : ICartService
{
    public async Task<Result<CartReadInfo>> GetMyCartAsync(int userId)
    {
        var res = await cartRepository.GetByUserIdAsync(userId);
        if (!res.IsSuccess)
            return Result<CartReadInfo>.Failure(res.Error);

        Cart cart = res.Value!;

        return Result<CartReadInfo>.Success(cart.ToReadInfo());
    }

    public async Task<BaseResult> AddItemAsync(int userId, CartItemCreateInfo createInfo)
    {
        var cartRes = await cartRepository.GetByUserIdAsync(userId);
        if (!cartRes.IsSuccess)
            return BaseResult.Failure(cartRes.Error);

        var productRes = await productRepository.GetByIdAsync(createInfo.ProductId);
        if(!productRes.IsSuccess)
            return BaseResult.Failure(productRes.Error);

        Cart cart = cartRes.Value!;
        Product product = productRes.Value!;
        
        if(product.StockPerKg < createInfo.QuantityKg)
            return BaseResult.Failure(Error.BadRequest("Not enough product in stock"));

        CartItem? existingCartItem = cart.CartItems
            .FirstOrDefault(ci=>ci.ProductId == createInfo.ProductId);

        if (existingCartItem != null)
        {
            existingCartItem.QuantityKg += createInfo.QuantityKg;
            return BaseResult.Success();
        }

        CartItem newItem = createInfo.ToEntity(cart.Id,product.PricePerKg);

        await cartItemRepository.AddAsync(newItem);
        return BaseResult.Success();
    }

    public async Task<BaseResult> UpdateItemAsync(int userId,int cartItemId, CartItemUpdateInfo updateInfo)
    {
        var cartRes = await cartRepository.GetByUserIdAsync(userId);
        if (!cartRes.IsSuccess)
            return BaseResult.Failure(cartRes.Error);

        CartItem? item = cartRes.Value!.CartItems
            .FirstOrDefault(ci => ci.Id == cartItemId);
        
        if(item is null)
            return BaseResult.Failure(Error.NotFound());

        var productRes = await productRepository.GetByIdAsync(item.ProductId);
        if(!productRes.IsSuccess)
            return BaseResult.Failure(productRes.Error);
        
        if(productRes.Value!.StockPerKg < updateInfo.QuantityKg)
            return BaseResult.Failure(Error.BadRequest("Not enough product in stock"));

        item.QuantityKg = updateInfo.QuantityKg;
        return BaseResult.Success();
    }

    public async Task<BaseResult> RemoveItemAsync(int userId, int cartItemId)
    {
        var cartRes = await cartRepository.GetByUserIdAsync(userId);
        if(!cartRes.IsSuccess)
            return BaseResult.Failure(cartRes.Error);

        CartItem? item = cartRes.Value!.CartItems
            .FirstOrDefault(ci => ci.Id == cartItemId);

        if (item is null)
            return BaseResult.Failure(Error.NotFound());

        await cartItemRepository.DeleteAsync(cartItemId);
        return BaseResult.Success();
    }

    public async Task<BaseResult> ClearCartAsync(int userId)
    {
        var cartRes = await cartRepository.GetByUserIdAsync(userId);
        if (!cartRes.IsSuccess)
            return BaseResult.Failure(cartRes.Error);

        foreach (var item in cartRes.Value!.CartItems)
            await cartItemRepository.DeleteAsync(item);
        
        return BaseResult.Success();
    }
}