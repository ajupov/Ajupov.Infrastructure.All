using System.IdentityModel.Tokens.Jwt;

namespace Ajupov.Infrastructure.All.Jwt.JwtReader
{
    public class JwtReader : IJwtReader
    {
        public JwtSecurityToken ReadAccessToken(string stringValue)
        {
            return new JwtSecurityTokenHandler().ReadToken(stringValue) as JwtSecurityToken;
        }
    }
}
