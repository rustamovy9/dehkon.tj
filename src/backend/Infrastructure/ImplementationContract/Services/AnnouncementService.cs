using Application.Contracts.IRepositories;
using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Extensions.Mappers;
using Application.Extensions.ResultPattern;
using Domain.Common;
using Domain.Entities;

namespace Infrastructure.ImplementationContract.Services;

public class AnnouncementService(IAnnouncementRepository repository) : IAnnouncementService
{
    public async Task<BaseResult> CreateAsync(int adminId, AnnouncementCreateInfo createInfo)
    {
        Announcement announcement = createInfo.ToEntity(adminId);

        Result<int> res = await repository.AddAsync(announcement);

        return res.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(res.Error);
    }

    public async Task<BaseResult> UpdateAsync(int id, AnnouncementUpdateInfo updateInfo)
    {
        Result<Announcement> existing = await repository.GetByIdAsync(id);
        if (!existing.IsSuccess)
            return BaseResult.Failure(existing.Error);

        Announcement announcement = existing.Value!.ToEntity(updateInfo);

        Result<int> res = await repository.UpdateAsync(announcement);

        return res.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(res.Error);
    }

    public async Task<BaseResult> DeleteAsync(int id)
    {
        Result<int> res = await repository.DeleteAsync(id);

        return res.IsSuccess
            ? BaseResult.Success()
            : BaseResult.Failure(res.Error);
    }

    public async Task<IEnumerable<AnnouncementReadInfo>> GetAllAsync()
    {
       Result<IEnumerable<Announcement>> res = await repository.GetAllAsync();
       if (!res.IsSuccess)
           return Enumerable.Empty<AnnouncementReadInfo>();

       return res.Value!
           .Select(a => a.ToRead());
    }
}