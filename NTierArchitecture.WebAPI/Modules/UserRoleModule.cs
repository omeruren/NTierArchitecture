using Carter;
using NTierArchitecture.Business.Users;
using NTierArchitecture.Entity.Dtos.Users;
using NTierArchitecture.WebAPI.Filters;
using TS.Result;

namespace NTierArchitecture.WebAPI.Modules;

public sealed class UserRoleModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder group)
    {
        var app = group.MapGroup("/user-roles")
            .WithTags("UserRoles")
            .RequireRateLimiting("fixed")
            .RequireAuthorization();

        // POST

        app.MapPost(string.Empty, async (
            UserRoleCreateDto request,
            UserRoleService service,
            CancellationToken token) =>
        {
            var response = await service.CreateAsync(request, token);
            return Results.Ok(response);
        }).Produces<Result<string>>().AddEndpointFilter<ValidationFilter<RoleCreateDto>>();

        // DELETE

        app.MapDelete("{id}", async (
            Guid id,
            UserRoleService service,
            CancellationToken token) =>
        {
            var response = await service.DeleteAsync(id, token);
            return Results.Ok(response);
        }).Produces<Result<string>>();

    }
}
