using FluentValidation;
using NTierArchitecture.Entity.Dtos.Products;

namespace NTierArchitecture.Business.Products;

public sealed class ProductCreateValidator : AbstractValidator<ProductCreateDto>
{
    public ProductCreateValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Product name sis required.");
        RuleFor(p => p.UnitPrice).GreaterThan(0).WithMessage("Unit price must be greater than 0.");

    }
}
