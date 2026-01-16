using Application.Contracts.IRepositories;
using Domain.Entities;
using Infrastructure.DataAccess;
using Infrastructure.ImplementationContract.Repositories.BaseRepository;

namespace Infrastructure.ImplementationContract.Repositories;

public class OrderRepository(DataContext dbContext)
: GenericRepository<Order>(dbContext),IOrderRepository;