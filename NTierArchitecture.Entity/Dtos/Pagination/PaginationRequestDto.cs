namespace NTierArchitecture.Entity.Dtos.Pagination;

public sealed record PaginationRequestDto(int PageNumber, int PageSize, string Search);
