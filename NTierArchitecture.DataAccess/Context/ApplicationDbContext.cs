using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NTierArchitecture.Entity.Abstractions;
using NTierArchitecture.Entity.Models;
using System.Security.Claims;

namespace NTierArchitecture.DataAccess.Context;

public sealed class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApplicationDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        this._httpContextAccessor = httpContextAccessor;
    }
    #region Models
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Role> AppRoles { get; set; }
    public DbSet<UserRole> AppUserRoles { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.Ignore<IdentityRole<Guid>>();
        modelBuilder.Ignore<IdentityRoleClaim<Guid>>();
        modelBuilder.Ignore<IdentityUserClaim<Guid>>();
        modelBuilder.Ignore<IdentityUserToken<Guid>>();
        modelBuilder.Ignore<IdentityUserLogin<Guid>>();
        modelBuilder.Ignore<IdentityUserRole<Guid>>();
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<AbstractEntity>();

        foreach (var entry in entries)
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value ?? throw new ArgumentException("user not found");

            if (entry.State == EntityState.Added)
            {
                entry.Property(p => p.CreatedAt).CurrentValue = DateTimeOffset.Now;
                entry.Property(p => p.CreatedUserId).CurrentValue = Guid.Parse(userId);

            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Property(p => p.UpdatedAt).CurrentValue = DateTimeOffset.Now;
                entry.Property(p => p.UpdatedUserId).CurrentValue = Guid.Parse(userId);

            }
            else if (entry.State == EntityState.Deleted)
            {
                if (!entry.Property(p => p.IsDeleted).CurrentValue)
                {
                    entry.Property(p => p.DeletedAt).CurrentValue = DateTimeOffset.Now;
                    entry.Property(p => p.IsDeleted).CurrentValue = true;
                    entry.State = EntityState.Modified;
                    entry.Property(p => p.DeletedUserId).CurrentValue = Guid.Parse(userId);
                }

            }
        }

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}
