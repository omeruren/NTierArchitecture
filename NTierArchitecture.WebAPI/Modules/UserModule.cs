using Carter;
using NTierArchitecture.Business.Users;
using NTierArchitecture.Entity.Dtos.Pagination;
using NTierArchitecture.Entity.Dtos.Users;
using NTierArchitecture.Entity.Models;
using NTierArchitecture.WebAPI.Filters;
using TS.Result;

namespace NTierArchitecture.WebAPI.Modules;

public sealed class UserModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder group)
    {
        var app = group.MapGroup("/users").WithTags("Users").RequireRateLimiting("fixed");

        // GET ALL

        app.MapGet(string.Empty, async (
            UserService service,
            int pageNumber = 1,
            int pageSize = 10,
            string search = "",
            CancellationToken token = default
            ) =>
        {
            var result = await service.GetAllAsync(new PaginationRequestDto(pageNumber, pageSize, search), token);
            return Results.Ok(result);
        }).Produces<Result<List<User>>>();

        // GET BY ID

        app.MapGet("{id}", async (
            Guid id,
            UserService service,
            CancellationToken token) =>
        {
            var result = await service.GetAsync(id, token);
            return Results.Ok(result);
        }).Produces<Result<User>>();

        // POST

        app.MapPost(string.Empty, async (
            UserCreateDto request,
            UserService service,
            CancellationToken token) =>
        {
            var result = await service.CreateAsync(request, token);
            return Results.Ok(result);
        }).Produces<Result<string>>().AddEndpointFilter<ValidationFilter<UserCreateDto>>();

        // PUT

        app.MapPut(string.Empty, async (
            UserUpdateDto request,
            UserService service,
            CancellationToken token) =>
        {
            var result = await service.UpdateAsync(request, token);
            return Results.Ok(result);
        }).Produces<Result<string>>().AddEndpointFilter<ValidationFilter<UserUpdateDto>>();

        // DELETE

        app.MapDelete("{id}", async (
            Guid id,
            UserService service,
            CancellationToken token) =>
        {
            var result = await service.DeleteAsync(id, token);
            return Results.Ok(result);
        }).Produces<Result<string>>();
    }
}
