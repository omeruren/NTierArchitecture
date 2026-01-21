using Mapster;
using Microsoft.EntityFrameworkCore;
using NTierArchitecture.Business.Abstractions;
using NTierArchitecture.DataAccess.Context;
using NTierArchitecture.Entity.Dtos.Pagination;
using NTierArchitecture.Entity.Dtos.Products;
using NTierArchitecture.Entity.Models;
using TS.Result;

namespace NTierArchitecture.Business.Products;

public sealed class ProductService(ApplicationDbContext _context)
{
    public async Task<Result<string>> CreateAsync(ProductCreateDto request, CancellationToken token)
    {
        bool isExist = await _context.Products.AnyAsync(p => p.Name == request.Name, token);
        if (isExist)
            throw new ArgumentException("Product is already exist");

        Product product = request.Adapt<Product>();
        _context.Products.Add(product);
        await _context.SaveChangesAsync(token);
        return "Product Created Successfully";
    }

    public async Task<Result<ProductResultDto>> GetAsync(Guid id, CancellationToken token)
    {
        ProductResultDto? product = await _context.Products
             .Where(p => p.Id == id)
             .LeftJoin(_context.Categories, m => m.CategoryId, m => m.Id, (product, category) => new { product, category })
             .LeftJoin(_context.Users, m => m.product.CreatedUserId, m => m.Id, (entity, user) => new { product = entity.product, category = entity.category, createdUser = user })
             .LeftJoin(_context.Users, m => m.product.UpdatedUserId, m => m.Id, (entity, user) => new { product = entity.product, category = entity.category, createdUser = entity.createdUser, updatedUser = user })
             .Select(s => new ProductResultDto
             {
                 Id = s.product.Id,
                 Name = s.product.Name,
                 CategoryId = s.product.CategoryId,
                 CategoryName = s.category!.Name,
                 UnitPrice = s.product.UnitPrice,
                 CreatedAt = s.product.CreatedAt,
                 CreatedUserId = s.product.CreatedUserId,
                 CreatedUserName = s.createdUser!.FullName,
                 UpdatedAt = s.product.UpdatedAt,
                 UpdatedUserId = s.product.UpdatedUserId,
                 UpdatedUserName = (s.product.UpdatedAt == null ? null : s.updatedUser!.FullName)!,
                 IsDeleted = s.product.IsDeleted,
                 DeletedAt = s.product.DeletedAt,
                 DeletedUserId = s.product.DeletedUserId,
                 DeletedUserName = ""
             })
             .FirstOrDefaultAsync(token) ?? throw new ArgumentException("Product not found");
        return product;
    }
    public async Task<Result<PaginationResponseDto<ProductResultDto>>> GetAllAsync(PaginationRequestDto request, CancellationToken token)
    {
        return await _context.Products
             .Where(p => p.Name.Contains(request.Search))
             .LeftJoin(_context.Categories, p => p.CategoryId, p => p.Id, (product, category) => new { product, category })
             .LeftJoin(_context.Users, m => m.product.CreatedUserId, m => m.Id, (entity, user) => new
             {
                 product = entity.product,
                 category = entity.category,
                 createdUser = user
             })
             .LeftJoin(_context.Users, m => m.product.UpdatedUserId, m => m.Id, (entity, user) => new
             {
                 product = entity.product,
                 category = entity.category,
                 createdUser = entity.createdUser,
                 updatedUser = user
             })
             .Select(s => new ProductResultDto
             {
                 Id = s.product.Id,
                 Name = s.product.Name,
                 CategoryName = s.category!.Name,
                 CategoryId = s.product.CategoryId,
                 UnitPrice = s.product.UnitPrice,

                 CreatedAt = s.product.CreatedAt,
                 CreatedUserId = s.product.CreatedUserId,
                 CreatedUserName = s.createdUser!.FullName,

                 UpdatedAt = s.product.UpdatedAt,
                 UpdatedUserId = s.product.UpdatedUserId,
                 UpdatedUserName = s.product.UpdatedAt == null ? null : s.updatedUser!.FullName,

                 IsDeleted = s.product.IsDeleted,
                 DeletedAt = s.product.DeletedAt,
                 DeletedUserId = s.product.DeletedUserId,
                 DeletedUserName = s.product.DeletedAt == null ? null : s.updatedUser.FullName
             })
             .OrderBy(p => p.Name)
             .Pagination(request, token);


    }

    public async Task<Result<string>> UpdateAsync(ProductUpdateDto request, CancellationToken token)
    {
        Product? product = await _context.Products.FindAsync(request.Id, token) ?? throw new ArgumentException("Product not found");

        if (product.Name != request.Name)
        {
            bool isExist = await _context.Products.AnyAsync(p => p.Name == request.Name, token);
            if (isExist)
                throw new ArgumentException("Product is already exist");
        }

        request.Adapt(product);

        _context.Products.Update(product);
        await _context.SaveChangesAsync(token);
        return "Product Created Successfully";

    }

    public async Task<Result<string>> DeleteAsync(Guid id, CancellationToken token)
    {
        Product? product = await _context.Products.FindAsync(id, token) ?? throw new ArgumentException("Product not found");
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return "Product Deleted Successfully";

    }
}

