using System.Linq.Expressions;
using Application.Contracts.IRepositories;
using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Extensions.Mappers;
using Application.Extensions.Responses.PagedResponse;
using Application.Extensions.ResultPattern;
using Application.Filters;
using Domain.Entities;
using Infrastructure.Extensions;

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

    public async Task<Result<PagedResponse<IEnumerable<AnnouncementReadInfo>>>> GetAllAsync(AnnouncementFilter filter)
    {
        Expression<Func<Announcement, bool>> filterExpression = announcement =>
            (filter.CreatedFrom == null || announcement.CreatedAt >= filter.CreatedFrom) &&
            (filter.CreatedTo == null || announcement.CreatedAt <= filter.CreatedTo) &&
            (filter.CreatedBy == null || announcement.UserId == filter.CreatedBy);

        var request = await repository.Find(filterExpression);

        if (!request.IsSuccess)
            return Result<PagedResponse<IEnumerable<AnnouncementReadInfo>>>.Failure(request.Error);

        List<AnnouncementReadInfo> query = request.Value!
            .Select(a=>a.ToRead()).ToList();

        int count = query.Count;

        IEnumerable<AnnouncementReadInfo> announcements =
            query.Page(filter.PageNumber, filter.PageSize);

        PagedResponse<IEnumerable<AnnouncementReadInfo>> response =
            PagedResponse<IEnumerable<AnnouncementReadInfo>>
                .Create(filter.PageNumber, filter.PageSize, count, announcements);

        return Result<PagedResponse<IEnumerable<AnnouncementReadInfo>>>.Success(response);
    }
}