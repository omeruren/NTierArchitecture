using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NTierArchitecture.Business.Abstractions;
using NTierArchitecture.DataAccess.Context;
using NTierArchitecture.Entity.Dtos.Category;
using NTierArchitecture.Entity.Dtos.Pagination;
using NTierArchitecture.Entity.Models;
using TS.Result;

namespace NTierArchitecture.Business.Categories;

public sealed class CategoryService(ApplicationDbContext _context, IMemoryCache _memoryCache)
{
    public async Task<Result<string>> CreateAsync(CategoryCreateDto request, CancellationToken token)
    {
        bool isExist = await _context.Categories.AnyAsync(c => c.Name == request.Name, token);

        if (isExist)
            throw new ArgumentException("Category already exists.");

        Category category = request.Adapt<Category>();
        _context.Categories.Add(category);
        await _context.SaveChangesAsync(token);
        _memoryCache.Remove("categories");
        return "Category Created Successfully.";
    }

    public async Task<Result<CategoryResultDto>> GetAsync(Guid id, CancellationToken token)
    {
        CategoryResultDto? category = await _context.Categories
            .Where(c => c.Id == id)
            .LeftJoin(_context.Users, m => m.CreatedUserId, m => m.Id, (category, user) => new { category, user })
            .Select(c => new CategoryResultDto
            {
                Id = c.category.Id,
                Name = c.category.Name,

                CreatedAt = c.category.CreatedAt,
                CreatedUserId = c.category.CreatedUserId,
                CreatedUserName = c.user!.FullName,

                UpdatedAt = c.category.UpdatedAt,
                UpdatedUserId = c.category.UpdatedUserId,
                UpdatedUserName = (c.category.UpdatedAt == null ? null : c.user!.FullName)!,

                IsDeleted = c.category.IsDeleted,
                DeletedAt = c.category.DeletedAt,
                DeletedUserName = (c.category.DeletedAt == null ? null : c.user.FullName)!

            })
            .FirstOrDefaultAsync(token) ?? throw new ArgumentException("Category not found.");

        return category;
    }

    public async Task<Result<PaginationResponseDto<CategoryResultDto>>> GetAllAsync(PaginationRequestDto request, CancellationToken token)
    {
        var categories = _memoryCache.Get<PaginationResponseDto<CategoryResultDto>>("categories");

        if (categories is null)
        {
            categories = await _context.Categories
                .LeftJoin(_context.Users, m => m.CreatedUserId, m => m.Id, (category, user) => new { category, user })
                .Select(c => new CategoryResultDto
                {
                    Id = c.category.Id,
                    Name = c.category.Name,

                    CreatedAt = c.category.CreatedAt,
                    CreatedUserId = c.category.CreatedUserId,
                    CreatedUserName = c.user!.FullName,

                    UpdatedAt = c.category.UpdatedAt,
                    UpdatedUserId = c.category.UpdatedUserId,
                    UpdatedUserName = (c.category.UpdatedAt == null ? null : c.user!.FullName)!,

                    IsDeleted = c.category.IsDeleted,
                    DeletedAt = c.category.DeletedAt,
                    DeletedUserName = (c.category.DeletedAt == null ? null : c.user.FullName)!
                })
                     .OrderBy(c => c.Name).Pagination(request, token);

            _memoryCache.Set("categories", categories);
        }
        return categories;
    }

    public async Task<Result<string>> UpdateAsync(CategoryUpdateDto request, CancellationToken token)
    {
        Category? category = await _context.Categories.FindAsync(request.Id, token) ?? throw new ArgumentException("Category not found.");

        if (request.Name != category.Name)
        {
            bool isExist = await _context.Categories.AnyAsync(c => c.Name == request.Name, token);
            if (isExist)
                throw new ArgumentException("Category already exists.");
        }

        request.Adapt(category);

        _context.Categories.Update(category);
        await _context.SaveChangesAsync(token);
        _memoryCache.Remove("categories");

        return "Category Updated Successfully.";

    }

    public async Task<Result<string>> DeleteAsync(Guid id, CancellationToken token)
    {
        Category? category = await _context.Categories.FindAsync(id, token) ?? throw new ArgumentException("Category not found.");
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(token);
        _memoryCache.Remove("categories");
        return "Category Deleted Successfully.";

    }
}
