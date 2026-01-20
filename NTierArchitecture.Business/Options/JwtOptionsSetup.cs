using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace NTierArchitecture.Business.Options;

public sealed class JwtOptionsSetup(IOptions<JwtOptions> _jwtOptions) : IPostConfigureOptions<JwtBearerOptions>
{
    public void PostConfigure(string? name, JwtBearerOptions options)
    {
        string secertKey = _jwtOptions.Value.SecretKey;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secertKey));
        options.TokenValidationParameters.ValidateIssuer = true;
        options.TokenValidationParameters.ValidateAudience = true;
        options.TokenValidationParameters.ValidateIssuerSigningKey = true;
        options.TokenValidationParameters.ValidateLifetime = true;
        options.TokenValidationParameters.ValidIssuer = _jwtOptions.Value.Issuer;
        options.TokenValidationParameters.ValidAudience = _jwtOptions.Value.Audience;
        options.TokenValidationParameters.IssuerSigningKey = securityKey;

    }
}
