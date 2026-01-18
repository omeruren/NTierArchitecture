
using Carter;
using Microsoft.AspNetCore.RateLimiting;
using NTierArchitecture.Business.Extensions;
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

app.MapCarter();
app.Run();
