using NTierArchitecture.Entity.Abstractions;

namespace NTierArchitecture.Entity.Models;

public sealed class Product : AbstractEntity
{
    public string Name { get; set; } = default!;
    public decimal UnitPrice { get; set; }
    public Guid CategoryId { get; set; }

}
