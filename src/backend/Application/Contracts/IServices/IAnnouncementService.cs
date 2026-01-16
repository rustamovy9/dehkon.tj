using Application.DTO_s;
using Application.Extensions.ResultPattern;

namespace Application.Contracts.IServices;

public interface IAnnouncementService
{
    Task<BaseResult> CreateAsync(int adminId, AnnouncementCreateInfo createInfo);
    Task<BaseResult> UpdateAsync(int id, AnnouncementUpdateInfo updateInfo);
    Task<BaseResult> DeleteAsync(int id);
    Task<IEnumerable<AnnouncementReadInfo>> GetAllAsync();
}