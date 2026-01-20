using Carter;
using NTierArchitecture.Business.Users;
using TS.Result;

namespace NTierArchitecture.WebAPI.Modules;

public sealed class AuthModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder group)
    {
        var app = group.MapGroup("/auth").WithTags("Auth").RequireRateLimiting("auth-fixed");

        app.MapPost("login", async (
            string userName,
            string password,
            AuthService authService,
            CancellationToken token) =>
        {
            var result = await authService.LoginAsync(userName, password, token);
            return Results.Ok(result);
        }).Produces<Result<string>>();
    }
}
