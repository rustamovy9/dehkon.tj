using Application.DTO_s;
using Application.Extensions.ResultPattern;

namespace Application.Contracts.IServices;

public interface IReviewService
{
    Task<BaseResult> CreatAsync(int userId, ReviewCreateInfo createInfo);
    Task<Result<ReviewReadInfo>> GetByProductAsync(int productId);
    Task<BaseResult> DeleteAsync(int id,int userId);
}