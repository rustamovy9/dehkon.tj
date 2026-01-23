using Application.Contracts.IRepositories.IBaseRepository;
using Application.Extensions.ResultPattern;
using Domain.Entities;

namespace Application.Contracts.IRepositories;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Result<Order>> GetByIdWithItemAsync(int orderId);
    Task<Result<IEnumerable<Order>>> GetAvailableForCourierAsync();
    Task<Result<IEnumerable<Order>>> GetCourierOrdersAsync(int courier);
}