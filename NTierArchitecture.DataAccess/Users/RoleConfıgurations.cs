using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NTierArchitecture.Entity.Models;

namespace NTierArchitecture.DataAccess.Users;

internal sealed class RoleConfıgurations : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasIndex(r => r.Name).IsUnique();
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
internal sealed class UserRoleConfıgurations : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(u => new { u.UserId, u.RoleId });
    }
}
