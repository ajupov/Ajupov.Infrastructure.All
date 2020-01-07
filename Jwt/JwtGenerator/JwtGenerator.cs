using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Ajupov.Infrastructure.All.Jwt.Helpers;
using Ajupov.Infrastructure.All.Jwt.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Ajupov.Infrastructure.All.Jwt.JwtGenerator
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly JwtSettings _settings;

        public JwtGenerator(IOptions<JwtSettings> options)
        {
            _settings = options.Value;
        }

        public string Generate(IEnumerable<Claim> claims, TimeSpan expiresTimeSpan)
        {
            var now = DateTime.UtcNow;
            var expires = now.Add(expiresTimeSpan);
            var securityKey = SymmetricSecurityKeyHelper.GetSymmetricSecurityKey(_settings.Key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(_settings.Issuer, _settings.Audience, claims, now, expires, credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}