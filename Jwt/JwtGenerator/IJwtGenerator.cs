using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Ajupov.Infrastructure.All.Jwt.JwtGenerator
{
    public interface IJwtGenerator
    {
        string Generate(string key, string audience, IEnumerable<Claim> claims, TimeSpan expiresTimeSpan);
    }
}