using Microsoft.AspNetCore.Diagnostics;
using NTierArchitecture.WebAPI.Filters;
using System.Net;
using TS.Result;

namespace NTierArchitecture.WebAPI;

public sealed class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is ValidationExceptionEx validationException)
        {
            var errors = validationException.Errors.SelectMany(s => s.Value).ToList();
            var validResponse = Result<string>.Failure(422, errors);

            httpContext.Response.StatusCode = 422;
            await httpContext.Response.WriteAsJsonAsync(validResponse);

            return true;
        }

        var result = Result<string>.Failure(exception.Message);
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;//500

        await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);

        return true;
    }
}
