using NTierArchitecture.Entity.Abstractions;

namespace NTierArchitecture.Entity.Dtos.Orders;

public class OrderResponseDto : AbstractEntity
{
    public DateTimeOffset OrderDate { get; set; }
    public int Quantity { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = default!;

}
