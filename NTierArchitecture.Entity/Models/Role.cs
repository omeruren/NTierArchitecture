using NTierArchitecture.Entity.Abstractions;

namespace NTierArchitecture.Entity.Models;

public sealed class Role : AbstractEntity
{
    public string Name { get; set; } = default!;
}

public sealed class UserRole
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}
