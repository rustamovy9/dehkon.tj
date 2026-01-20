using Application.DTO_s;
using Application.Extensions.Responses.PagedResponse;
using Application.Extensions.ResultPattern;
using Application.Filters;

namespace Application.Contracts.IServices;

public interface IAnnouncementService
{
    Task<BaseResult> CreateAsync(int adminId, AnnouncementCreateInfo createInfo);
    Task<BaseResult> UpdateAsync(int id, AnnouncementUpdateInfo updateInfo);
    Task<BaseResult> DeleteAsync(int id);
    Task<Result<PagedResponse<IEnumerable<AnnouncementReadInfo>>>> GetAllAsync(AnnouncementFilter filter);
}