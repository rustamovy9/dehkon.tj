using Application.Contracts.IRepositories.IBaseRepository.ICrud;
using Domain.Common;

namespace Application.Contracts.IRepositories.IBaseRepository;

public interface IGenericRepository<T> :
    IGenericAddRepository<T>,
    IGenericUpdateRepository<T>,
    IGenericDeleteRepository<T>,
    IGenericFindRepository<T> where T : BaseEntity;