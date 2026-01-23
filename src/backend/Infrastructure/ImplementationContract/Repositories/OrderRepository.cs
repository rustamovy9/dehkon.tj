using System.Collections;
using Application.Contracts.IRepositories;
using Application.DTO_s;
using Application.Extensions.ResultPattern;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.DataAccess;
using Infrastructure.ImplementationContract.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ImplementationContract.Repositories;

public class OrderRepository(DataContext dbContext)
    : GenericRepository<Order>(dbContext), IOrderRepository
{
    private readonly DataContext _dbContext = dbContext;

    public async Task<Result<Order>> GetByIdWithItemAsync(int orderId)
    {
        try
        {
            Order? order = await _dbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order is null)
                return Result<Order>.Failure(Error.NotFound("Order not found"));

            return Result<Order>.Success(order);
        }
        catch (Exception e)
        {
            return Result<Order>.Failure(Error.InternalServerError(e.Message));
        }
    }

    public async Task<Result<IEnumerable<Order>>> GetAvailableForCourierAsync()
    {
        try
        {
            var orders = await _dbContext.Orders
                .Include(o => o.OrderItems)
                .Where(o =>
                    !o.IsDeleted &&
                    o.CourierId == null &&
                    o.Status == OrderStatus.Paid &&
                    o.IsActive)
                .OrderBy(o=>o.CreatedAt)
                .ToListAsync();

            return Result<IEnumerable<Order>>.Success(orders);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<Order>>.Failure(Error.InternalServerError(e.Message));
        }
    }

    public async Task<Result<IEnumerable<Order>>> GetCourierOrdersAsync(int courierId)
    {
        try
        {
            var orders = await _dbContext.Orders
                .Include(o => o.OrderItems)
                .Where(o =>
                    !o.IsDeleted &&
                    o.CourierId == courierId)
                .ToListAsync();

            return Result<IEnumerable<Order>>.Success(orders);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<Order>>.Failure(Error.InternalServerError(e.Message));
        }
    }
}