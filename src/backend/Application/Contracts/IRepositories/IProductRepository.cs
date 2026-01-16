using Application.Contracts.IRepositories.IBaseRepository;
using Domain.Entities;

namespace Application.Contracts.IRepositories;

public interface IProductRepository : IGenericRepository<Product>;