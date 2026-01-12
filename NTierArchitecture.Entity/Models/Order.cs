using NTierArchitecture.Entity.Abstractions;

namespace NTierArchitecture.Entity.Models;

public sealed class Order : AbstractEntity
{
    public DateTimeOffset OrderDate { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
