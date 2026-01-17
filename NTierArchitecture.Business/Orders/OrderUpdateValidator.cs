using FluentValidation;
using NTierArchitecture.Entity.Dtos.Orders;

namespace NTierArchitecture.Business.Orders;

public sealed class OrderUpdateValidator : AbstractValidator<OrderUpdateDto>
{
    public OrderUpdateValidator()
    {
        RuleFor(p => p.Quantity).GreaterThan(0).WithMessage("Birim fiyat 0 dan büyük olmalıdır.");

    }
}
