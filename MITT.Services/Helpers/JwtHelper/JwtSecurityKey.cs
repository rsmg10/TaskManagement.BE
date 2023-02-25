using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MITT.Services.Helpers.JwtHelper
{
    public static class JwtSecurityKey
    {
        public static SymmetricSecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}