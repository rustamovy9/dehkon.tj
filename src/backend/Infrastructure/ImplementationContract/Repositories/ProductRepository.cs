using Application.Contracts.IRepositories;
using Domain.Entities;
using Infrastructure.DataAccess;
using Infrastructure.ImplementationContract.Repositories.BaseRepository;

namespace Infrastructure.ImplementationContract.Repositories;

public class ProductRepository(DataContext dbContext)
: GenericRepository<Product>(dbContext),IProductRepository;