using Carter;
using NTierArchitecture.Business.Categories;
using NTierArchitecture.Entity.Dtos.Category;
using NTierArchitecture.Entity.Models;
using NTierArchitecture.WebAPI.Filters;
using TS.Result;

namespace NTierArchitecture.WebAPI.Modules;

public sealed class CategoryModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder group)
    {
        var app = group.MapGroup("/categories").WithTags("Categories");

        // GET ALL
        app.MapGet(string.Empty, async (
            CategoryService _service,
            CancellationToken token) =>
        {
            var result = await _service.GetAllAsync(token);
            return Results.Ok(result);
        }).Produces<Result<List<Category>>>();

        // GET BY ID

        app.MapGet("/{id}", async (
            Guid id,
            CancellationToken token,
            CategoryService _service) =>
        {
            var result = await _service.GetAsync(id, token);
            return Results.Ok(result);
        }).Produces<Result<Category>>();

        // POST

        app.MapPost(string.Empty, async (
            CategoryCreateDto request,
            CancellationToken token,
            CategoryService _service) =>
        {
            var result = await _service.CreateAsync(request, token);
            return Results.Ok(result);
        }).Produces<string>().AddEndpointFilter<ValidationFilter<CategoryCreateDto>>();


        // PUT

        app.MapPut(string.Empty, async (
            CategoryUpdateDto request,
            CancellationToken token,
            CategoryService _service) =>
        {
            var result = await _service.UpdateAsync(request, token);
            return Results.Ok(result);
        }).Produces<Result<string>>().AddEndpointFilter<ValidationFilter<CategoryUpdateDto>>();

        // DELETE

        app.MapDelete("/{id}", async (
            Guid id,
            CancellationToken token,
            CategoryService _service) =>
        {
            var result = await _service.DeleteAsync(id, token);
            return Results.Ok(result);
        }).Produces<Result<string>>();
    }
}
