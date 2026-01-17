
using Carter;
using NTierArchitecture.Business.Extensions;
using NTierArchitecture.DataAccess.Extensions;
using NTierArchitecture.WebAPI;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccess(builder.Configuration).AddBusiness();
builder.Services.AddCarter();
builder.Services.AddOpenApi();

builder.Services.AddExceptionHandler<ExceptionHandler>().AddProblemDetails();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();
app.UseExceptionHandler();

app.MapCarter();
app.Run();
