using Carter;
using NTierArchitecture.Business.Orders;
using NTierArchitecture.Entity.Dtos.Orders;
using NTierArchitecture.Entity.Models;

namespace NTierArchitecture.WebAPI.Modules;

public sealed class OrderModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder group)
    {
        var app = group.MapGroup("/orders").WithTags("Orders");

        app.MapGet(string.Empty, async (
            OrderService _service,
            CancellationToken token
            ) =>
        {
            var result = await _service.GetAllAsync(token);
            return Results.Ok(result);
        }).Produces<List<Order>>();

        app.MapGet("/{id}", async (
            Guid id,
            OrderService _service,
            CancellationToken token
            ) =>
        {
            var result = await _service.GetAsync(id, token);
            return Results.Ok(result);
        }).Produces<Order>();

        app.MapPost(string.Empty, async (
            OrderCreateDto request,
            OrderService _service,
            CancellationToken token
            ) =>
        {
            await _service.CreateAsync(request, token);
            return Results.Created();
        });

        app.MapPut(string.Empty, async (
            OrderUpdateDto request,
            OrderService _service,
            CancellationToken token
            ) =>
        {
            await _service.UpdateAsync(request, token);
            return Results.Ok();
        });

        app.MapDelete("/{id}", async (
            Guid id,
            OrderService _service,
            CancellationToken token
            ) =>
        {
            await _service.DeleteAsync(id, token);
            return Results.Ok();
        });
    }
}
