using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NTierArchitecture.DataAccess.Context;
using NTierArchitecture.Entity.Dtos.Users;
using NTierArchitecture.Entity.Models;
using TS.Result;

namespace NTierArchitecture.Business.Users;

public sealed class RoleService(ApplicationDbContext _context, IMemoryCache _memoryCache)
{
    public async Task<Result<string>> CreateAsync(RoleCreateDto request, CancellationToken token)
    {
        bool isNameExists = await _context.AppRoles.AnyAsync(u => u.Name == request.Name, token);

        if (isNameExists)
            throw new ArgumentException("Role name is already exists");

        Role role = request.Adapt<Role>();
        await _context.AppRoles.AddAsync(role, token);
        await _context.SaveChangesAsync(token);
        _memoryCache.Remove("roles");
        return "Role created successfully";
    }

    public async Task<Result<Role>> GetAsync(Guid id, CancellationToken token)
    {
        Role? role = await _context.AppRoles.FindAsync(id, token) ?? throw new ArgumentException("Role not found");

        return role;
    }

    public async Task<Result<List<Role>>> GetAllAsync(CancellationToken token)
    {
        var roles = _memoryCache.Get<List<Role>>("roles");

        if (roles is null)
        {
            roles = await _context.AppRoles.OrderBy(r => r.Name).ToListAsync(token);
            _memoryCache.Set("roles", roles);
        }
        return roles;
    }

    public async Task<Result<string>> UpdateAsync(RoleUpdateDto request, CancellationToken token)
    {
        Role? role = await _context.AppRoles.FindAsync(request.Id, token) ?? throw new ArgumentException("Role not found");

        if (request.Name != role.Name)
        {
            bool isNameExists = await _context.AppRoles.AnyAsync(r => r.Name == request.Name, token);
            if (isNameExists)
                throw new ArgumentException("Name is already exists");
            request.Adapt(role);
            _context.AppRoles.Update(role);

            await _context.SaveChangesAsync(token);

        }
        _memoryCache.Remove("roles");
        return "Role updated successfully";
    }

    public async Task<Result<string>> DeleteAsync(Guid id, CancellationToken token)
    {
        Role? role = await _context.AppRoles.FindAsync(id, token) ?? throw new ArgumentException("Role not found");

        _context.AppRoles.Remove(role);
        await _context.SaveChangesAsync(token);
        _memoryCache.Remove("roles");
        return "Role deleted successfully";
    }
}
