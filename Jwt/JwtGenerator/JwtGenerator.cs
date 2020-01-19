using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Ajupov.Infrastructure.All.Jwt.Helpers;
using Microsoft.IdentityModel.Tokens;

namespace Ajupov.Infrastructure.All.Jwt.JwtGenerator
{
    public class JwtGenerator : IJwtGenerator
    {
        public string Generate(string key, string audience, IEnumerable<Claim> claims, TimeSpan expiresTimeSpan)
        {
            var now = DateTime.UtcNow;
            var expires = now.Add(expiresTimeSpan);
            var securityKey = SymmetricSecurityKeyHelper.GetSymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(JwtDefaults.AuthenticationScheme, audience, claims, now, expires, credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}