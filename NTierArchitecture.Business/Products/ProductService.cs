using Microsoft.EntityFrameworkCore;
using NTierArchitecture.DataAccess.Context;
using NTierArchitecture.Entity.Dtos.Products;
using NTierArchitecture.Entity.Models;

namespace NTierArchitecture.Business.Products;

public sealed class ProductService(ApplicationDbContext _context)
{
    public async Task CreateAsync(ProductCreateDto request, CancellationToken token)
    {
        bool isExist = await _context.Products.AnyAsync(p => p.Name == request.Name, token);
        if (isExist)
            throw new ArgumentException("Product is already exist");
        Product product = new()
        {
            Name = request.Name,
            UnitPrice = request.UnitPrice,
            CategoryId = request.CategoryId
        };
        _context.Products.Add(product);
        await _context.SaveChangesAsync(token);
    }

    public async Task<Product> GetAsync(Guid id, CancellationToken token)
    {
        Product? product = await _context.Products.FindAsync(id, token) ?? throw new ArgumentException("Product not found");

        return product;
    }
    public async Task<List<Product>> GetAllAsync(CancellationToken token)
    {
        return await _context.Products
            .OrderBy(p => p.Name)
            .ToListAsync(token);
    }

    public async Task UpdateAsync(ProductUpdateDto request, CancellationToken token)
    {
        Product? product = await _context.Products.FindAsync(request.Id, token) ?? throw new ArgumentException("Product not found");

        if (product.Name != request.Name)
        {
            bool isExist = await _context.Products.AnyAsync(p => p.Name == request.Name, token);
            if (isExist)
                throw new ArgumentException("Product is already exist");
        }
        product.Name = request.Name;
        product.UnitPrice = request.UnitPrice;
        product.CategoryId = request.CategoryId;

        _context.Products.Update(product);
        await _context.SaveChangesAsync(token);
    }

    public async Task DeleteAsync(Guid id, CancellationToken token)
    {
        Product? product = await _context.Products.FindAsync(id, token) ?? throw new ArgumentException("Product not found");
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}

