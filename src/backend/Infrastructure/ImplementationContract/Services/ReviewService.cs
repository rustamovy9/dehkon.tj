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
using Infrastructure.Extensions;

namespace Infrastructure.ImplementationContract.Services;

public class ReviewService(IReviewRepository repository,IOrderRepository orderRepository) : IReviewService
{
    public async Task<BaseResult> CreatAsync(int userId, ReviewCreateInfo createInfo)
    {
        var reviewRes = await orderRepository.Find(o => o.BuyerId == userId &&
                                                     o.OrderItems.Any(i => i.ProductId == createInfo.ProductId));

        bool bought = reviewRes.Value!.Any();
        
        if(!bought)
            return BaseResult.Failure(Error.BadRequest("You can review only purchased products"));

        var unique = await  repository.Find(r => r.UserId == userId &&
                                          r.ProductId == createInfo.ProductId);
        bool exist = unique.Value!.Any();
        if(exist)
            return BaseResult.Failure(Error.Conflict("Review already exists"));

        Review review = createInfo.ToEntity(userId);

        var res =await repository.AddAsync(review);
        return res.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(res.Error);
    }

    public async Task<BaseResult> UpdateAsync(int id,int userId, ReviewUpdateInfo updateInfo)
    {
        var reviewRes = await repository.GetByIdAsync(id);
        if (!reviewRes.IsSuccess)
            return BaseResult.Failure(reviewRes.Error);

        Review review = reviewRes.Value!;
        
        if(review.UserId != userId)
            return BaseResult.Failure(Error.Forbidden());

        review.ToEntity(updateInfo);

        var res = await repository.UpdateAsync(review);

        return res.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(res.Error);
    }

    public async Task<Result<IEnumerable<ReviewReadInfo>>> GetByProductAsync(int productId)
    {
        var request = await repository.Find(r => r.ProductId == productId);

        if (!request.IsSuccess)
            return Result<IEnumerable<ReviewReadInfo>>.Failure(request.Error);

        return Result<IEnumerable<ReviewReadInfo>>.Success(request.Value!.Select(r=>r.ToRead()));
    }

    public async Task<Result<PagedResponse<IEnumerable<ReviewReadInfo>>>> GetAllAsync(ReviewFilter filter)
    {
        Expression<Func<Review, bool>> filterExpression = r =>
            (filter.ProductId == null || r.ProductId == filter.ProductId) &&
            (filter.Rating == null || r.Rating == filter.Rating);

        var request = await repository.Find(filterExpression);

        if (!request.IsSuccess)
            return Result<PagedResponse<IEnumerable<ReviewReadInfo>>>.Failure(request.Error);

        List<ReviewReadInfo> query = request.Value!
            .OrderByDescending(r => r.CreatedAt)
            .Select(r=>r.ToRead())
            .ToList();

        int count = query.Count;

        IEnumerable<ReviewReadInfo> items =
            query.Page(filter.PageNumber, filter.PageSize);

        var response = PagedResponse<IEnumerable<ReviewReadInfo>>
            .Create(filter.PageSize, filter.PageNumber, count, items);

        return Result<PagedResponse<IEnumerable<ReviewReadInfo>>>.Success(response);
    }

    public async Task<BaseResult> DeleteAsync(int id, int userId)
    {
        var reviewRes = await repository.GetByIdAsync(id);
        if(!reviewRes.IsSuccess)
            return BaseResult.Failure(reviewRes.Error);

        if (reviewRes.Value!.UserId != userId)
            return BaseResult.Failure(Error.Forbidden());

        var res = await repository.DeleteAsync(id);

        return res.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(res.Error);
    }
}