using Application.Contracts.IRepositories;
using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Extensions.Mappers;
using Application.Extensions.ResultPattern;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.ImplementationContract.Services;

public class CourierService(IOrderRepository orderRepository) : ICourierService
{
    public async Task<Result<IEnumerable<OrderShortReadInfo>>> GetAvailableOrdersAsync()
    {
        var res = await orderRepository.GetAvailableForCourierAsync();

        return res.IsSuccess
            ? Result<IEnumerable<OrderShortReadInfo>>.Success(res.Value!.Select(o => o.ToReadShort()))
            : Result<IEnumerable<OrderShortReadInfo>>.Failure(res.Error);
    }

    public async Task<BaseResult> TakeOrderAsync(int courierId, int orderId)
    {
        var orderRes = await orderRepository.GetByIdAsync(orderId);
        if (!orderRes.IsSuccess)
            return BaseResult.Failure(orderRes.Error);

        var order = orderRes.Value!;

        if (order.CourierId != null)
            return BaseResult.Failure(Error.BadRequest("Order already taken"));

        if (order.Status != OrderStatus.Paid)
            return BaseResult.Failure(Error.BadRequest("Order not ready"));

        order.CourierId = courierId;
        order.Status = OrderStatus.AssignedCourier;
        order.UpdatedAt = DateTimeOffset.UtcNow;

        var res = await orderRepository.UpdateAsync(order);
        return res.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(res.Error);
    }

    public async Task<BaseResult> DeliverOrderAsync(int courierId, int orderId)
    {
        var orderRes = await orderRepository.GetByIdAsync(orderId);
        if (!orderRes.IsSuccess)
            return BaseResult.Failure(orderRes.Error);

        Order order = orderRes.Value!;

        if (order.CourierId != courierId)
            return BaseResult.Failure(Error.Forbidden());

        if (order.Status != OrderStatus.InDelivery)
            return BaseResult.Failure(Error.BadRequest("Order is not in delivery"));

        order.Status = OrderStatus.Delivered;
        order.DeliveredAt = DateTimeOffset.UtcNow;
        order.UpdatedAt = DateTimeOffset.UtcNow;
        order.Version++;

        var res = await orderRepository.UpdateAsync(order);
        return res.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(res.Error);
    }

    public async Task<Result<IEnumerable<OrderShortReadInfo>>> GetMyOrdersAsync(int courierId)
    {
        var res = await orderRepository.GetCourierOrdersAsync(courierId);

        return res.IsSuccess
            ? Result<IEnumerable<OrderShortReadInfo>>.Success(res.Value!.Select(o => o.ToReadShort()))
            : Result<IEnumerable<OrderShortReadInfo>>.Failure(res.Error);
    }
}