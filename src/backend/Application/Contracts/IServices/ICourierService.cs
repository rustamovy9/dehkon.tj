using Application.DTO_s;
using Application.Extensions.ResultPattern;

namespace Application.Contracts.IServices;

public interface ICourierService
{
    Task<Result<IEnumerable<OrderShortReadInfo>>> GetAvailableOrdersAsync();
    Task<BaseResult> TakeOrderAsync(int courierId, int orderId);
    Task<BaseResult> DeliverOrderAsync(int courierId, int orderId);
    Task<Result<IEnumerable<OrderShortReadInfo>>> GetMyOrdersAsync(int courierId);
}