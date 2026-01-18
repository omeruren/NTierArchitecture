using Microsoft.EntityFrameworkCore;
using NTierArchitecture.Entity.Models;

namespace NTierArchitecture.DataAccess.Users;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
    {
        builder.Property(p => p.FirstName).HasColumnType("varchar(100)");
        builder.Property(p => p.LastName).HasColumnType("varchar(100)");
    }
}
