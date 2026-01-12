using Microsoft.EntityFrameworkCore;

namespace NTierArchitecture.DataAccess.Context;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
}
