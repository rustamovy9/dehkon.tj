using Domain.Common;

namespace Application.Extensions.Responses.PagedResponse;

public record PagedResponse<T> : BaseFilter
{
    public int TotalPage { get; init; }
    public int TotalRecords { get; init; }
    public T? Data { get; init; }

    private PagedResponse(int pageSize, int pageNumber, int totalRecords, T? data) : base(pageSize, pageNumber)
    {
        Data = data;
        TotalRecords = totalRecords;
        TotalPage = (int)Math.Ceiling((double)totalRecords/pageSize);
    }

    public static PagedResponse<T> Create(int pageSize, int pageNumber, int totalRecords, T? data)
        => new PagedResponse<T>(pageSize,pageNumber,totalRecords,data);
}