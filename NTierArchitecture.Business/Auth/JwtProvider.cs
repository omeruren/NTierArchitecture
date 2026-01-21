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
    public string CreateToken(User user, List<string> roles)
    {
        string secertKey = _options.Value.SecretKey;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secertKey));

        var signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

        var claims = new List<Claim>()
        {
          new(ClaimTypes.NameIdentifier,user.Id.ToString()),
          new("userName",user.UserName!),
          new("fullName",user.FullName),
          new("email",user.Email!),
        };
        foreach (var role in roles)
        {
            var claim = new Claim(ClaimTypes.Role, role);
            claims.Add(claim);
        }
        JwtSecurityToken jwtSecurityToken = new(
            claims: claims,
            issuer: _options.Value.Issuer,
            audience: _options.Value.Audience,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: signinCredentials
            );
        JwtSecurityTokenHandler handler = new();
        var token = handler.WriteToken(jwtSecurityToken);
        return token;
    }
}
