using Application.Contracts.IRepositories;
using Application.Contracts.IServices;
using Application.Extensions.ResultPattern;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.ImplementationContract.Services;

public class PaymentService(IOrderRepository orderRepository,IProductRepository productRepository) : IPaymentService
{
    public async Task<BaseResult> PayOrderAsync(int userId, int orderId)
    {
        var orderRes = await orderRepository.GetByIdWithItemAsync(orderId);
        if (!orderRes.IsSuccess)
            return BaseResult.Failure(orderRes.Error);

        Order order = orderRes.Value!;

        if (order.BuyerId != userId)
            return BaseResult.Failure(Error.Forbidden());

        if (order.Status != OrderStatus.AwaitingPayment)
            return BaseResult.Failure(Error.BadRequest("Order is not awaiting payment"));

        foreach (var item in order.OrderItems)
        {
            if (item.Product.StockPerKg < item.QuantityKg)
                return BaseResult.Failure(Error.BadRequest($"Not enough stock for {item.Product.Name}"));
        }

        foreach (var item in order.OrderItems)
        {
            item.Product.StockPerKg -= item.QuantityKg;
            await productRepository.UpdateAsync(item.Product);
        }

        order.Status = OrderStatus.Paid;
        order.PaymentStatus = PaymentStatus.Paid;
        order.PaidAt = DateTimeOffset.UtcNow;
        order.UpdatedAt = DateTimeOffset.UtcNow;
        order.Version++;

        await orderRepository.UpdateAsync(order);
        
        return BaseResult.Success();
    }
}