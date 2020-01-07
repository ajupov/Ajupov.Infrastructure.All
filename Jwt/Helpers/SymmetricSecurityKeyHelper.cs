using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Ajupov.Infrastructure.All.Jwt.Helpers
{
    public static class SymmetricSecurityKeyHelper
    {
        public static SymmetricSecurityKey GetSymmetricSecurityKey(string value)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(value));
        }
    }
}