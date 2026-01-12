using Microsoft.EntityFrameworkCore;
using NTierArchitecture.DataAccess.Context;
using NTierArchitecture.Entity.Dtos.Category;
using NTierArchitecture.Entity.Models;

namespace NTierArchitecture.Business.Categories;

public sealed class CategoryService(ApplicationDbContext _context)
{
    public async Task CreateAsync(CategoryCreateDto request, CancellationToken token)
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
    }
}
