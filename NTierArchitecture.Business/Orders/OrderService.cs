using Mapster;
using Microsoft.EntityFrameworkCore;
using NTierArchitecture.Business.Abstractions;
using NTierArchitecture.DataAccess.Context;
using NTierArchitecture.Entity.Dtos.Orders;
using NTierArchitecture.Entity.Dtos.Pagination;
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
            .LeftJoin(_context.Users, o => o.order.CreatedUserId, o => o.Id, (entity, user) => new
            {
                entity.order,
                entity.product,
                createdUser = user
            })
            .LeftJoin(_context.Users, o => o.order.UpdatedUserId, o => o.Id, (entity, user) => new
            {
                entity.order,
                entity.product,
                entity.createdUser,
                updatedUser = user
            })
            .Select(s => new OrderResponseDto
            {
                Id = s.order.Id,
                OrderDate = s.order.OrderDate,
                Quantity = s.order.Quantity,

                ProductId = s.order.ProductId,
                ProductName = s.product!.Name,

                CreatedAt = s.order.CreatedAt,
                CreatedUserId = s.order.CreatedUserId,
                CreatedUserName = s.createdUser!.FullName,

                UpdatedAt = s.order.UpdatedAt,
                UpdatedUserId = s.order.UpdatedUserId,
                UpdatedUserName = (s.order.UpdatedAt == null ? null : s.updatedUser!.FullName)!,

                IsDeleted = s.product.IsDeleted,
                DeletedAt = s.product.DeletedAt,
                DeletedUserName = (s.order.DeletedAt == null ? null : s.updatedUser!.FullName)!
            })
            .FirstOrDefaultAsync(token) ?? throw new ArgumentException("Order not found");

        return order;
    }
    public async Task<Result<PaginationResponseDto<OrderResponseDto>>> GetAllAsync(PaginationRequestDto request, CancellationToken token)
    {
        return await _context.Orders
            .LeftJoin(_context.Products, o => o.ProductId, o => o.Id, (order, product) => new { order, product })
            .LeftJoin(_context.Users, o => o.order.CreatedUserId, o => o.Id, (entity, user) => new
            {
                entity.order,
                entity.product,
                createdUser = user
            })
            .LeftJoin(_context.Users, o => o.order.UpdatedUserId, o => o.Id, (entity, user) => new
            {
                entity.order,
                entity.product,
                entity.createdUser,
                updatedUser = user
            })
            .Select(s => new OrderResponseDto
            {
                Id = s.order.Id,
                OrderDate = s.order.OrderDate,
                Quantity = s.order.Quantity,

                ProductId = s.order.ProductId,
                ProductName = s.product!.Name,

                CreatedUserId = s.order.CreatedUserId,
                CreatedUserName = s.createdUser!.FullName,
                CreatedAt = s.product.CreatedAt,

                UpdatedUserId = s.order.UpdatedUserId,
                UpdatedUserName = (s.order.UpdatedAt == null ? null : s.updatedUser!.FullName)!,
                UpdatedAt = s.order.UpdatedAt,

                IsDeleted = s.product.IsDeleted,
                DeletedAt = s.product.DeletedAt,
                DeletedUserId = s.order.DeletedUserId,
                DeletedUserName = (s.order.DeletedAt == null ? null : s.updatedUser!.FullName)!
            })
            .OrderBy(o => o.OrderDate)
            .Pagination(request, token);
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
