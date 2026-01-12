using Carter;
using NTierArchitecture.Business.Products;
using NTierArchitecture.Entity.Dtos.Products;
using NTierArchitecture.Entity.Models;

namespace NTierArchitecture.WebAPI.Modules;

public class ProductModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder group)
    {
        var app = group.MapGroup("/products").WithTags("Products");

        app.MapGet(string.Empty, async (
            ProductService _service,
            CancellationToken token
            ) =>
        {
            var result = await _service.GetAllAsync(token);
            return Results.Ok(result);
        }).Produces<List<Product>>();

        app.MapGet("/{id}", async (
            Guid id,
            ProductService _service,
            CancellationToken token) =>
        {
            var result = await _service.GetAsync(id, token);
            return Results.Ok(result);
        }).Produces<Product>();

        app.MapPost(string.Empty, async (
            ProductService _service,
            ProductCreateDto request,
            CancellationToken token) =>
        {
            await _service.CreateAsync(request, token);
            return Results.Created();
        });

        app.MapPut(string.Empty, async (
            ProductService _service,
            ProductUpdateDto request,
            CancellationToken token) =>
        {
            await _service.UpdateAsync(request, token);
            return Results.Ok();
        });

        app.MapDelete("/{id}", async (
            Guid id,
            ProductService _service,
            CancellationToken token) =>
        {
            await _service.DeleteAsync(id, token);
            return Results.Ok();
        });

    }
}
