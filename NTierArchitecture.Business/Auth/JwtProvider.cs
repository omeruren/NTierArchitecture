using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NTierArchitecture.Entity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NTierArchitecture.Business.Auth;

public sealed class JwtProvider(IConfiguration configuration)
{
    public string CreateToken(User user)
    {
        string secertKey = configuration.GetSection("Jwt:SecretKey").Value!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secertKey));

        var signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

        JwtSecurityToken jwtSecurityToken = new(
            issuer: configuration.GetSection("Jwt:Issuer").Value!,
            audience: configuration.GetSection("Jwt:Audience").Value!,
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
