using Mapster;
using NTierArchitecture.DataAccess.Context;
using NTierArchitecture.Entity.Dtos.Users;
using NTierArchitecture.Entity.Models;
using TS.Result;

namespace NTierArchitecture.Business.Users;

public sealed class UserRoleService(ApplicationDbContext _context)
{
    public async Task<Result<string>> CreateAsync(UserRoleCreateDto request, CancellationToken token)
    {
        UserRole userRole = request.Adapt<UserRole>();

        _context.AppUserRoles.Add(userRole);
        await _context.SaveChangesAsync(token);
        return "User Role saved successfully";
    }
    public async Task<Result<string>> DeleteAsync(Guid id, CancellationToken token)
    {
        UserRole? userRole = await _context.AppUserRoles.FindAsync(id, token) ?? throw new ArgumentException("User role not found");

        _context.AppUserRoles.Remove(userRole);
        await _context.SaveChangesAsync(token);
        return "User Role deleted successfully";
    }


}
