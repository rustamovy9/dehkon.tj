using Application.Contracts.IRepositories;
using Domain.Entities;
using Infrastructure.DataAccess;
using Infrastructure.ImplementationContract.Repositories.BaseRepository;

namespace Infrastructure.ImplementationContract.Repositories;

public class CartItemRepository(DataContext dbContext)
: GenericRepository<CartItem>(dbContext),ICartItemRepository;