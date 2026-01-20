
using Carter;
using Microsoft.AspNetCore.RateLimiting;
using NTierArchitecture.Business.Extensions;
using NTierArchitecture.Business.Options;
using NTierArchitecture.DataAccess.Extensions;
using NTierArchitecture.WebAPI;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccess(builder.Configuration).AddBusiness();
builder.Services.AddCarter();
builder.Services.AddOpenApi();

builder.Services.AddExceptionHandler<ExceptionHandler>().AddProblemDetails();

builder.Services.AddCors();

builder.Services.AddResponseCompression(x => x.EnableForHttps = true);

builder.Services.AddRateLimiter(x =>
{
    x.AddFixedWindowLimiter("fixed", cfr =>
    {
        cfr.PermitLimit = 50;
        cfr.Window = TimeSpan.FromSeconds(5);
        cfr.QueueLimit = 50;
        cfr.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
    });
});

builder.Services.AddRateLimiter(x =>
{
    x.AddFixedWindowLimiter("auth-fixed", cfr =>
    {
        cfr.PermitLimit = 5;
        cfr.Window = TimeSpan.FromMinutes(1);
        cfr.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
    });
});

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .SetPreflightMaxAge(TimeSpan.FromMinutes(10))
);

app.UseResponseCompression();

app.UseExceptionHandler();


app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();
app.MapCarter();
app.Run();
