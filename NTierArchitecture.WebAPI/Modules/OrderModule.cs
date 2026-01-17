using Carter;
using NTierArchitecture.Business.Orders;
using NTierArchitecture.Entity.Dtos.Orders;
using NTierArchitecture.WebAPI.Filters;
using TS.Result;

namespace NTierArchitecture.WebAPI.Modules;

public sealed class OrderModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder group)
    {
        var app = group.MapGroup("/orders").WithTags("Orders");

        // GET ALL

        app.MapGet(string.Empty, async (
            OrderService _service,
            CancellationToken token
            ) =>
        {
            var result = await _service.GetAllAsync(token);
            return Results.Ok(result);
        }).Produces<Result<List<OrderResponseDto>>>();

        // GET BY ID

        app.MapGet("/{id}", async (
            Guid id,
            OrderService _service,
            CancellationToken token
            ) =>
        {
            var result = await _service.GetAsync(id, token);
            return Results.Ok(result);
        }).Produces<Result<OrderResponseDto>>();

        // POST

        app.MapPost(string.Empty, async (
            OrderCreateDto request,
            OrderService _service,
            CancellationToken token
            ) =>
        {
            var result = await _service.CreateAsync(request, token);
            return Results.Ok(result);
        }).Produces<Result<string>>().AddEndpointFilter<ValidationFilter<OrderCreateDto>>();

        // PUT

        app.MapPut(string.Empty, async (
            OrderUpdateDto request,
            OrderService _service,
            CancellationToken token
            ) =>
        {
            var result = await _service.UpdateAsync(request, token);
            return Results.Ok(result);
        }).Produces<Result<string>>().AddEndpointFilter<ValidationFilter<OrderUpdateDto>>();

        // DELETE 

        app.MapDelete("/{id}", async (
            Guid id,
            OrderService _service,
            CancellationToken token
            ) =>
        {
            var result = await _service.DeleteAsync(id, token);
            return Results.Ok(result);
        }).Produces<Result<string>>();
    }
}
