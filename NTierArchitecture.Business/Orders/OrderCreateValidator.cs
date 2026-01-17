using FluentValidation;
using NTierArchitecture.Entity.Dtos.Orders;

namespace NTierArchitecture.Business.Orders;

public sealed class OrderCreateValidator : AbstractValidator<OrderCreateDto>
{
    public OrderCreateValidator()
    {
        RuleFor(p => p.Quantity).GreaterThan(0).WithMessage("Order quantity must be greater than 0.");
    }
}
