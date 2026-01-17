using FluentValidation;
using NTierArchitecture.Entity.Dtos.Products;

namespace NTierArchitecture.Business.Products;

public sealed class ProductUpdateValidator : AbstractValidator<ProductUpdateDto>
{
    public ProductUpdateValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Ürün adı boş kalamaz.");
        RuleFor(p => p.UnitPrice).GreaterThan(0).WithMessage("Birim fiyat 0 dan büyük olmalıdır.");

    }
}
