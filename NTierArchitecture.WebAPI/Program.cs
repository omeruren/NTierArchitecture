
using NTierArchitecture.Business.Extensions;
using NTierArchitecture.DataAccess.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccess(builder.Configuration).AddBusiness();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
