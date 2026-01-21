using Microsoft.EntityFrameworkCore;
using NTierArchitecture.DataAccess.Context;
using System.Security.Claims;

namespace NTierArchitecture.WebAPI.Filters;

public static class AuthorizationEndpointExtensions
{
    public static RouteHandlerBuilder RequireRoleFromDb(this RouteHandlerBuilder builder, string roleName)
    {
        return builder.AddEndpointFilterFactory((routeContext, next) =>
        {
            return async invocationContext =>
            {
                var http = invocationContext.HttpContext;
                if (http.User?.Identity?.IsAuthenticated != true)
                    return Results.Unauthorized();

                var userId = http.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                    return Results.Forbid();

                var dbContext = http.RequestServices.GetRequiredService<ApplicationDbContext>();

                var role = await dbContext.AppRoles.FirstOrDefaultAsync(p => p.Name == roleName);
                if (role is null)
                    return Results.Forbid();

                var hasRole = await dbContext.AppUserRoles.AnyAsync(x => x.UserId == Guid.Parse(userId) && x.RoleId == role.Id);

                if (!hasRole)
                    return Results.Forbid();

                return await next(invocationContext);

            };
        });
    }
}
