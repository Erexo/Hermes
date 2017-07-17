using Hermes.Infrastructure.Extensions;
using Hermes.Infrastructure.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hermes.Infrastructure.Services
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSettings _jwtSettings;

        public JwtHandler(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string getJwtToken(Guid id)
        {
            var now = DateTime.Now;
            var expires = now.AddMinutes(_jwtSettings.ExpirationMinutes);

            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToEpoch().ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, now.ToEpoch().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, expires.ToEpoch().ToString())
            };

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.PrivateKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                claims: claims,
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
