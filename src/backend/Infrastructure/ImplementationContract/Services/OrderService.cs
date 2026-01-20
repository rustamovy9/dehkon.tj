using System.Linq.Expressions;
using Application.Contracts.IRepositories;
using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Extensions.Mappers;
using Application.Extensions.Responses.PagedResponse;
using Application.Extensions.ResultPattern;
using Application.Filters;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Extensions;

namespace Infrastructure.ImplementationContract.Services;

public class OrderService(IOrderRepository orderRepository,ICartRepository cartRepository,IProductRepository productRepository) : IOrderService
{
    public async Task<BaseResult> CreateOrderAsync(int userId, OrderCreateInfo createInfo)
    {
        var cartRes = await cartRepository.GetByUserIdAsync(userId);
        if(!cartRes.IsSuccess)
            return BaseResult.Failure(cartRes.Error);

        Cart cart = cartRes.Value!;

        if (!cart.CartItems.Any())
            return BaseResult.Failure(Error.BadRequest("Cart is empty"));

        Order order = createInfo.ToEntity(userId);

        decimal totalPrice = 0;

        foreach (var item in cart.CartItems)
        {
            Product product = item.Product;

            if (product.StockPerKg < item.QuantityKg)
                return BaseResult.Failure(Error.BadRequest($"Not enough stock for {product.Name}"));

            OrderItem orderItem = new OrderItem
            {
                ProductId = product.Id,
                QuantityKg = item.QuantityKg,
                PricePerKg = item.PriceAtMoment
            };

            totalPrice += orderItem.TotalPrice;
            product.StockPerKg -= item.QuantityKg;

            order.OrderItems.Add(orderItem);
        }

        order.TotalPrice = totalPrice;

        await orderRepository.AddAsync(order);
        
        cart.CartItems.Clear();

        return BaseResult.Success();
    }

    public async Task<Result<PagedResponse<IEnumerable<OrderShortReadInfo>>>> GetOrdersAsync(OrderFilter orderFilter)
    {
        Expression<Func<Order, bool>> filterExpression = o =>
            (orderFilter.Status == null || o.Status == orderFilter.Status) &&
            (orderFilter.BuyerId == null || o.BuyerId == orderFilter.BuyerId) &&
            (orderFilter.CourierId == null || o.CourierId == orderFilter.CourierId) &&
            (orderFilter.DateFrom == null || o.CreatedAt >= orderFilter.DateFrom) &&
            (orderFilter.DateTo == null || o.CreatedAt <= orderFilter.DateTo) &&
            (orderFilter.SellerId == null || 
             o.OrderItems.Any(i => i.Product.SellerId == orderFilter.SellerId));

        var request = await orderRepository.Find(filterExpression);

        if (!request.IsSuccess)
            return Result<PagedResponse<IEnumerable<OrderShortReadInfo>>>.Failure(request.Error);

        List<OrderShortReadInfo> query = request.Value!
            .OrderByDescending(o => o.CreatedAt)
            .Select(o => o.ToReadShort())
            .ToList();

        int count = query.Count;

        IEnumerable<OrderShortReadInfo> items =
            query.Page(orderFilter.PageNumber, orderFilter.PageSize);

        var response = PagedResponse<IEnumerable<OrderShortReadInfo>>
            .Create(orderFilter.PageNumber, orderFilter.PageSize, count, items);

        return Result<PagedResponse<IEnumerable<OrderShortReadInfo>>>.Success(response);
    }

    public async Task<Result<PagedResponse<IEnumerable<OrderShortReadInfo>>>> GetMyOrdersAsync(int userId)
    {
        var request = await orderRepository.Find(o => o.BuyerId == userId);

        if (!request.IsSuccess)
            return Result<PagedResponse<IEnumerable<OrderShortReadInfo>>>.Failure(request.Error);

        List<OrderShortReadInfo> query = request.Value!
            .OrderByDescending(o => o.CreatedAt)
            .Select(o => o.ToReadShort())
            .ToList();

        int count = query.Count;

        var response = PagedResponse<IEnumerable<OrderShortReadInfo>>
            .Create(1, count, count, query);
        return Result<PagedResponse<IEnumerable<OrderShortReadInfo>>>.Success(response);
    }

    public async Task<Result<OrderDetailReadInfo>> GetOrderByIdAsync(int id)
    {
        var res = await orderRepository.GetByIdAsync(id);
        if (!res.IsSuccess)
            return Result<OrderDetailReadInfo>.Failure(res.Error);

        return Result<OrderDetailReadInfo>.Success(res.Value!.ToReadDetail());
    }

    public async Task<BaseResult> ChangeStatusAsync(int orderId, OrderStatus status)
    {
        var res = await orderRepository.GetByIdAsync(orderId);
        if(!res.IsSuccess)
            return BaseResult.Failure(res.Error);

        res.Value!.Status = status;
        return BaseResult.Success();
    }
}