using FluentValidation;
using NTierArchitecture.Entity.Dtos.Orders;

namespace NTierArchitecture.Business.Orders;

public sealed class OrderCreateValidator : AbstractValidator<OrderCreateDto>
{
    public OrderCreateValidator()
    {
        RuleFor(p => p.Quantity).GreaterThan(0).WithMessage("Sipariş adedi 0 dan büyük olmalıdır.");

    }
}
