
using Carter;
using NTierArchitecture.Business.Extensions;
using NTierArchitecture.DataAccess.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccess(builder.Configuration).AddBusiness();
builder.Services.AddCarter();
builder.Services.AddOpenApi();


var app = builder.Build();

app.MapCarter();
app.MapOpenApi();
app.MapScalarApiReference();
app.Run();
