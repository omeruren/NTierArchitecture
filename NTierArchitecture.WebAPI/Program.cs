
using Carter;
using NTierArchitecture.Business.Extensions;
using NTierArchitecture.DataAccess.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccess(builder.Configuration).AddBusiness();
builder.Services.AddCarter();
var app = builder.Build();

app.MapCarter();

app.Run();
