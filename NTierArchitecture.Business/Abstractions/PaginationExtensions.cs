using Microsoft.EntityFrameworkCore;
using NTierArchitecture.Entity.Dtos.Pagination;

namespace NTierArchitecture.Business.Abstractions;

public static class PaginationExtensions
{
    public static async Task<PaginationResponseDto<T>> Pagination<T>(this IQueryable<T> values, PaginationRequestDto request, CancellationToken token)
    {
        var list = await values
             .Skip((request.PageNumber - 1) * request.PageSize)
             .Take(request.PageSize)
             .ToListAsync(token);

        var pagRes = new PaginationResponseDto<T>();
        pagRes.Data = list;
        pagRes.PageNumber = request.PageNumber;
        pagRes.PageSize = request.PageSize;
        var totalPageCount = values.Count();
        pagRes.TotalPageCount = Math.Floor((decimal)totalPageCount / request.PageSize);

        return pagRes;
    }
}
