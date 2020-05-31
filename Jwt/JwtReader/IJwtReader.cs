using System.IdentityModel.Tokens.Jwt;

namespace Ajupov.Infrastructure.All.Jwt.JwtReader
{
    public interface IJwtReader
    {
        JwtSecurityToken ReadAccessToken(string stringValue);
    }
}