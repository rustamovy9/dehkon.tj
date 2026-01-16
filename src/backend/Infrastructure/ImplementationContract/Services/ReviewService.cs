using Application.Contracts.IRepositories;
using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Extensions.ResultPattern;

namespace Infrastructure.ImplementationContract.Services;

public class ReviewService(IReviewRepository repository) : IReviewService
{
    public Task<BaseResult> CreatAsync(int userId, ReviewCreateInfo createInfo)
    {
        throw new NotImplementedException();
    }

    public Task<Result<ReviewReadInfo>> GetByProductAsync(int productId)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult> DeleteAsync(int id, int userId)
    {
        throw new NotImplementedException();
    }
}