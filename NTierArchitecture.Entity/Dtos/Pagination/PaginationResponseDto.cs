namespace NTierArchitecture.Entity.Dtos.Pagination;

public sealed class PaginationResponseDto<T>
{
    public List<T> Data { get; set; } = default!;
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public decimal TotalPageCount { get; set; }

}