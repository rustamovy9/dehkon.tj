using Application.Extensions.ResultPattern;

namespace Application.Contracts.IServices;

public interface IPaymentService
{
    Task<BaseResult> PayOrderAsync(int userId,int orderId);
}