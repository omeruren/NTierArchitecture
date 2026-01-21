using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NTierArchitecture.DataAccess.Context;
using NTierArchitecture.Entity.Models;
using TS.Result;

namespace NTierArchitecture.Business.Auth;

public sealed class AuthService(UserManager<User> _userManager, JwtProvider jwtProvider, ApplicationDbContext _context)
{
    public async Task<Result<string>> LoginAsync(string userName, string password, CancellationToken token)
    {
        var user = await _userManager.FindByNameAsync(userName) ?? throw new Exception("User name or password is incorrect");

        var result = await _userManager.CheckPasswordAsync(user, password);

        if (!result)
            throw new Exception("User name or password is incorrect");

        var roles = await _context.AppUserRoles
            .Where(u => u.UserId == user.Id)
            .LeftJoin(_context.AppRoles, m => m.RoleId, m => m.Id, (userRole, role) => role)
            .Select(s => s!.Name)
            .ToListAsync(token);

        return jwtProvider.CreateToken(user, roles!);
    }
}
