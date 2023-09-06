using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api_gaming_global.Models.Crud
{
    public static class Auth
    {
        public static object GenerateJWTToken(IEnumerable<Claim> claims)
        {
            var JWTSecretToken = Environment.GetEnvironmentVariable("JWTSecretToken");
            var JWTDomainName = Environment.GetEnvironmentVariable("JWTDomainName");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSecretToken));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: JWTDomainName,
                audience: JWTDomainName,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
