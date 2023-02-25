using System.IdentityModel.Tokens.Jwt;

namespace MITT.Services.Helpers.JwtHelper
{
    public sealed class JwtToken
    {
        private readonly JwtSecurityToken _token;

        internal JwtToken(JwtSecurityToken token, string refreshToken, string systemIdentity, int tag = -1)
        {
            _token = token;
            RefreshToken = refreshToken;
            SystemIdentity = systemIdentity;
            Tag = tag;
        }

        public DateTime ValidTo => _token.ValidTo;
        public string RefreshToken { get; private set; }
        public string SystemIdentity { get; private set; }

        public int Tag { get; private set; }

        public string Value => new JwtSecurityTokenHandler().WriteToken(_token);
    }
}