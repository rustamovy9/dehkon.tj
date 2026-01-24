using System.Linq.Expressions;
using Application.Contracts.IRepositories.IBaseRepository;
using Application.Extensions.ResultPattern;
using Domain.Entities;

namespace Application.Contracts.IRepositories;

public interface IReviewRepository : IGenericRepository<Review>
{
    Task<Result<IEnumerable<Review>>> FindWithUserAsync(Expression<Func<Review, bool>> expression);
}