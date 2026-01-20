using Carter;
using NTierArchitecture.Business.Users;
using NTierArchitecture.Entity.Dtos.Users;
using NTierArchitecture.Entity.Models;
using NTierArchitecture.WebAPI.Filters;
using TS.Result;

namespace NTierArchitecture.WebAPI.Modules;

public sealed class RoleModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder group)
    {
        var app = group
            .MapGroup("/roles")
            .WithTags("Roles")
            .RequireRateLimiting("fixed")
            .RequireAuthorization();

        // GET ALL

        app.MapGet("{id}", async (
            Guid id,
            RoleService service,
            CancellationToken cancellation) =>
        {
            var res = await service.GetAsync(id, cancellation);
            return Results.Ok(res);
        }).Produces<Result<Role>>();

        // GET BY ID

        app.MapGet(string.Empty, async (
            RoleService service,
            CancellationToken cancellation) =>
        {
            var res = await service.GetAllAsync(cancellation);
            return Results.Ok(res);
        }).Produces<Result<List<Role>>>();


        // POST 

        app.MapPost(string.Empty, async (
            RoleCreateDto request,
            RoleService service,
            CancellationToken cancellation) =>
        {
            var res = await service.CreateAsync(request, cancellation);
            return Results.Ok(res);
        }).Produces<Result<string>>().AddEndpointFilter<ValidationFilter<RoleCreateDto>>();


        // PUT

        app.MapPut(string.Empty, async (
             RoleUpdateDto request,
             RoleService service,
             CancellationToken cancellation) =>
        {
            var res = await service.UpdateAsync(request, cancellation);
            return Results.Ok(res);
        }).Produces<Result<string>>().AddEndpointFilter<ValidationFilter<RoleUpdateDto>>();

        // DELETE

        app.MapDelete("{id}", async (
            Guid id,
            RoleService service,
            CancellationToken cancellation) =>
        {
            var res = await service.DeleteAsync(id, cancellation);
            return Results.Ok(res);
        }).Produces<Result<string>>();
    }
}

