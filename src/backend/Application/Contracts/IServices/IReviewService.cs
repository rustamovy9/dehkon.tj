using Application.DTO_s;
using Application.Extensions.Responses.PagedResponse;
using Application.Extensions.ResultPattern;
using Application.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Application.Contracts.IServices;

public interface IReviewService
{
    Task<BaseResult> CreatAsync(int userId, ReviewCreateInfo createInfo);
    Task<BaseResult> UpdateAsync(int id,int userId, ReviewUpdateInfo updateInfo);
    Task<BaseResult> DeleteAsync(int id,int userId);
    Task<Result<IEnumerable<ReviewReadInfo>>> GetByProductAsync(int productId);
    Task<Result<PagedResponse<IEnumerable<ReviewReadInfo>>>> GetAllAsync(ReviewFilter filter);
}