using FluentValidation;
using NTierArchitecture.Entity.Dtos.Products;

namespace NTierArchitecture.Business.Products;

public sealed class ProductUpdateValidator : AbstractValidator<ProductUpdateDto>
{
    public ProductUpdateValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Product name sis required.");
        RuleFor(p => p.UnitPrice).GreaterThan(0).WithMessage("Unit price must be greater than 0.");
    }
}
