using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NTierArchitecture.Business.Options;
using NTierArchitecture.Entity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NTierArchitecture.Business.Auth;

public sealed class JwtProvider(IOptions<JwtOptions> _options)
{
    public string CreateToken(User user)
    {
        string secertKey = _options.Value.SecretKey;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secertKey));

        var signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

        JwtSecurityToken jwtSecurityToken = new(
            issuer: _options.Value.Issuer,
            audience: _options.Value.Audience,
            claims: new List<Claim>()
            {
                new("userId",user.Id.ToString()),
                new("userName",user.UserName!),
                new("fullName",user.FullName),
                new("email",user.Email!)
            },
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: signinCredentials
            );
        JwtSecurityTokenHandler handler = new();
        var token = handler.WriteToken(jwtSecurityToken);
        return token;
    }
}
