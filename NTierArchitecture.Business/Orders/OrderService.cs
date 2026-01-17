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

        Order order = new()
        {
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            OrderDate = DateTimeOffset.Now
        };
        _context.Orders.Add(order);
        await _context.SaveChangesAsync(token);
        return "Order Created Successfully.";
    }

    public async Task<Result<Order>> GetAsync(Guid id, CancellationToken token)
    {
        Order? order = await _context.Orders.FindAsync(id, token) ?? throw new ArgumentException("Order not found");

        return order;
    }
    public async Task<Result<List<Order>>> GetAllAsync(CancellationToken token)
    {
        return await _context.Orders
            .ToListAsync(token);
    }

    public async Task<Result<string>> UpdateAsync(OrderUpdateDto request, CancellationToken token)
    {
        Order? order = await _context.Orders.FindAsync(request.Id, token) ?? throw new ArgumentException("Order not found");

        order.ProductId = request.ProductId;
        order.Quantity = request.Quantity;

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
