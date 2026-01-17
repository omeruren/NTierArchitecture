using Microsoft.EntityFrameworkCore;
using NTierArchitecture.DataAccess.Context;
using NTierArchitecture.Entity.Dtos.Category;
using NTierArchitecture.Entity.Models;
using TS.Result;

namespace NTierArchitecture.Business.Categories;

public sealed class CategoryService(ApplicationDbContext _context)
{
    public async Task<Result<string>> CreateAsync(CategoryCreateDto request, CancellationToken token)
    {
        bool isExist = await _context.Categories.AnyAsync(c => c.Name == request.Name, token);

        if (isExist)
            throw new ArgumentException("Category already exists.");

        Category category = new()
        {
            Name = request.Name
        };
        _context.Categories.Add(category);
        await _context.SaveChangesAsync(token);
        return "Category Created Successfully.";
    }

    public async Task<Result<Category>> GetAsync(Guid id, CancellationToken token)
    {
        Category? category = await _context.Categories.FindAsync(id, token) ?? throw new ArgumentException("Category not found.");

        return category;
    }

    public async Task<Result<List<Category>>> GetAllAsync(CancellationToken token)
    {
        return await _context.Categories
             .OrderBy(c => c.Name)
             .ToListAsync(token);
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
        category.Name = request.Name;
        _context.Categories.Update(category);
        await _context.SaveChangesAsync(token);
        return "Category Updated Successfully.";

    }

    public async Task<Result<string>> DeleteAsync(Guid id, CancellationToken token)
    {
        Category? category = await _context.Categories.FindAsync(id, token) ?? throw new ArgumentException("Category not found.");
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(token);
        return "Category Deleted Successfully.";

    }
}
