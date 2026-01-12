using Carter;
using NTierArchitecture.Business.Categories;
using NTierArchitecture.Entity.Dtos.Category;

namespace NTierArchitecture.WebAPI.Modules;

public sealed class CategoryModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder group)
    {
        var app = group.MapGroup("/categories").WithTags("Categories");

        app.MapPost(string.Empty, async (
            CategoryCreateDto request,
            CancellationToken token,
            CategoryService _service) =>
        {
            await _service.CreateAsync(request, token);
            return Results.Created();
        });

        app.MapGet("/{id}", async (
            Guid id,
            CancellationToken token,
            CategoryService _service) =>
        {
            var result = await _service.GetAsync(id, token);
            return Results.Ok(result);
        });

        app.MapGet(string.Empty, async (
            CategoryService _service,
            CancellationToken token) =>
        {
            var result = await _service.GetAllAsync(token);
            return Results.Ok(result);
        });

        app.MapPut(string.Empty, async (
            CategoryUpdateDto request,
            CancellationToken token,
            CategoryService _service) =>
        {
            await _service.UpdateAsync(request, token);
            return Results.Ok();
        });

        app.MapDelete("/{id}", async (
            Guid id,
            CancellationToken token,
            CategoryService _service) =>
        {
            await _service.DeleteAsync(id, token);
            return Results.Ok();
        });
    }
}
