using Microsoft.AspNetCore.Identity;
using NTierArchitecture.Entity.Models;
using TS.Result;

namespace NTierArchitecture.Business.Auth;

public sealed class AuthService(UserManager<User> _userManager, JwtProvider jwtProvider)
{
    public async Task<Result<string>> LoginAsync(string userName, string password, CancellationToken token)
    {
        var user = await _userManager.FindByNameAsync(userName) ?? throw new Exception("User name or password is incorrect");

        var result = await _userManager.CheckPasswordAsync(user, password);

        if (!result)
            throw new Exception("User name or password is incorrect");

        return jwtProvider.CreateToken(user);
    }
}
