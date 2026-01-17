using Microsoft.AspNetCore.Diagnostics;
using NTierArchitecture.WebAPI.Filters;
using System.Net;

namespace NTierArchitecture.WebAPI;

public sealed class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is ValidationExceptionEx validationException)
        {
            var errors = validationException.Errors.SelectMany(s => s.Value).ToList();

            httpContext.Response.StatusCode = 422;
            await httpContext.Response.WriteAsJsonAsync(errors);

            return true;
        }

        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;//500

        await httpContext.Response.WriteAsJsonAsync(new { exception.Message, httpContext.Response.StatusCode }, cancellationToken);

        return true;
    }
}
