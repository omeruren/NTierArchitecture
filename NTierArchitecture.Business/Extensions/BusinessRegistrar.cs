using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NTierArchitecture.Business.Categories;
using NTierArchitecture.Business.Orders;
using NTierArchitecture.Business.Products;

namespace NTierArchitecture.Business.Extensions;

public static class BusinessRegistrar
{
    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        // Register business services here
        services.AddMemoryCache();
        services.AddTransient<CategoryService>();
        services.AddTransient<ProductService>();
        services.AddTransient<OrderService>();
        services.AddValidatorsFromAssembly(typeof(BusinessRegistrar).Assembly);
        return services;
    }
}
