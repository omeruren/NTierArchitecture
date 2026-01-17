using FluentValidation;
using NTierArchitecture.Entity.Dtos.Products;

namespace NTierArchitecture.Business.Products;

public sealed class ProductCreateValidator : AbstractValidator<ProductCreateDto>
{
    public ProductCreateValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Ürün adı boş kalamaz.");
        RuleFor(p => p.UnitPrice).GreaterThan(0).WithMessage("Birim fiyat 0 dan büyük olmalıdır.");

    }
}
