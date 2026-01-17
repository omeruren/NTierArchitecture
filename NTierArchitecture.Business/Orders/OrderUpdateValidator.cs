using FluentValidation;
using NTierArchitecture.Entity.Dtos.Orders;

namespace NTierArchitecture.Business.Orders;

public sealed class OrderUpdateValidator : AbstractValidator<OrderUpdateDto>
{
    public OrderUpdateValidator()
    {
        RuleFor(p => p.Quantity).GreaterThan(0).WithMessage("Order quantity must be greater than 0.");
    }
}
