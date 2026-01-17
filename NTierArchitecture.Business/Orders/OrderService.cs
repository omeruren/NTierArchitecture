using Mapster;
using Microsoft.EntityFrameworkCore;
using NTierArchitecture.DataAccess.Context;
using NTierArchitecture.Entity.Dtos.Orders;
using NTierArchitecture.Entity.Models;
using TS.Result;

namespace NTierArchitecture.Business.Orders;

public sealed class OrderService(ApplicationDbContext _context)
{
    public async Task<Result<string>> CreateAsync(OrderCreateDto request, CancellationToken token)
    {

        Order order = request.Adapt<Order>();
        _context.Orders.Add(order);
        await _context.SaveChangesAsync(token);
        return "Order Created Successfully.";
    }

    public async Task<Result<OrderResponseDto>> GetAsync(Guid id, CancellationToken token)
    {
        OrderResponseDto? order = await _context.Orders
            .Where(o => o.Id == id)
            .LeftJoin(_context.Products, o => o.ProductId, o => o.Id, (order, product) => new { order, product })
            .Select(s => new OrderResponseDto
            {
                Id = s.order.Id,
                OrderDate = s.order.OrderDate,
                Quantity = s.order.Quantity,
                ProductId = s.order.ProductId,
                ProductName = s.product!.Name,
                CreatedAt = s.product.CreatedAt,
                UpdatedAt = s.product.UpdatedAt,
                IsDeleted = s.product.IsDeleted,
                DeletedAt = s.product.DeletedAt

            })
            .FirstOrDefaultAsync(token) ?? throw new ArgumentException("Order not found");

        return order;
    }
    public async Task<Result<List<OrderResponseDto>>> GetAllAsync(CancellationToken token)
    {
        return await _context.Orders
            .LeftJoin(_context.Products, o => o.ProductId, o => o.Id, (order, product) => new { order, product })
            .Select(s => new OrderResponseDto
            {
                Id = s.order.Id,
                OrderDate = s.order.OrderDate,
                Quantity = s.order.Quantity,
                ProductId = s.order.ProductId,
                ProductName = s.product!.Name,
                CreatedAt = s.product.CreatedAt,
                UpdatedAt = s.product.UpdatedAt,
                IsDeleted = s.product.IsDeleted,
                DeletedAt = s.product.DeletedAt
            })
            .OrderBy(o => o.OrderDate)
            .ToListAsync(token);
    }

    public async Task<Result<string>> UpdateAsync(OrderUpdateDto request, CancellationToken token)
    {
        Order? order = await _context.Orders.FindAsync(request.Id, token) ?? throw new ArgumentException("Order not found");

        request.Adapt(order);

        _context.Orders.Update(order);
        await _context.SaveChangesAsync(token);
        return "Order Updated Successfully.";

    }

    public async Task<Result<string>> DeleteAsync(Guid id, CancellationToken token)
    {
        Order? order = await _context.Orders.FindAsync(id, token) ?? throw new ArgumentException("Order not found");
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync(token);
        return "Order Deleted Successfully.";
    }
}
