using Application.Contracts.IRepositories;
using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Extensions.Responses.PagedResponse;
using Application.Extensions.ResultPattern;
using Domain.Enums;

namespace Infrastructure.ImplementationContract.Services;

public class OrderService(IOrderRepository repository) : IOrderService
{
    public Task<BaseResult> CreateOrderAsync(int userId, OrderCreateInfo createInfo)
    {
        throw new NotImplementedException();
    }

    public Task<Result<PagedResponse<IEnumerable<OrderShortReadInfo>>>> GetMyOrdersAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<OrderDetailReadInfo>> GetOrderByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult> ChangeStatusAsync(int orderId, OrderStatus status)
    {
        throw new NotImplementedException();
    }
}