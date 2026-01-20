using System.Linq.Expressions;
using Application.Contracts.IRepositories;
using Application.Extensions.ResultPattern;
using Domain.Common;
using Domain.Entities;
using Infrastructure.DataAccess;
using Infrastructure.ImplementationContract.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.ImplementationContract.Repositories;

public class UserRepository(DataContext dbContext)
    : GenericRepository<User>(dbContext), IUserRepository
{
    private readonly DataContext _dbContext = dbContext;

    public async Task<Result<User>> GetByIdWithRoleAsync(int id)
    {
        try
        {
            User? user = await _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user is null
                ? Result<User>.Failure(Error.NotFound())
                : Result<User>.Success(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<IEnumerable<User>>> FindWithRole(Expression<Func<User, bool>> expression)
    {
        try
        {
            List<User> data = await _dbContext.Set<User>()
                .Include(u=>u.Role)
                .Where(expression).ToListAsync();

            return Result<IEnumerable<User>>.Success(data);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<User>>.Failure(Error.InternalServerError());
        }
    }
}