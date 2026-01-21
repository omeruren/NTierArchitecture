using NTierArchitecture.Entity.Dtos.Abstract;

namespace NTierArchitecture.Entity.Dtos.Orders;

public class OrderResponseDto : AbstractEntityDto
{
    public DateTimeOffset OrderDate { get; set; }
    public int Quantity { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = default!;

}
