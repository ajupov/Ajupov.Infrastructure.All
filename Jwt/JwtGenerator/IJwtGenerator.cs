using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Ajupov.Infrastructure.All.Jwt.JwtGenerator
{
    public interface IJwtGenerator
    {
        string Generate(IEnumerable<Claim> claims, TimeSpan expiresTimeSpan);
    }
}