using Application.DTO_s;
using Application.Extensions.Responses.PagedResponse;
using Application.Extensions.ResultPattern;
using Domain.Enums;

namespace Application.Contracts.IServices;

public interface IOrderService
{
    Task<BaseResult> CreateOrderAsync(int userId, OrderCreateInfo createInfo);
    Task<Result<PagedResponse<IEnumerable<OrderShortReadInfo>>>> GetMyOrdersAsync(int userId);
    Task<Result<OrderDetailReadInfo>> GetOrderByIdAsync(int id);
    Task<BaseResult> ChangeStatusAsync(int orderId,OrderStatus status);
}