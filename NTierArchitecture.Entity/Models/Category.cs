using NTierArchitecture.Entity.Abstractions;

namespace NTierArchitecture.Entity.Models;

public sealed class Category : AbstractEntity
{
    public string Name { get; set; } = default!;
}
