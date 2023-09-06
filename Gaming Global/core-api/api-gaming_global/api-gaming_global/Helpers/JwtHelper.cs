using api_gaming_global.Models.Request;
using Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api_gaming_global.Helpers
{
    public class JwtHelper
    {
        private ILogger<SqlHelper> _logger;
        private Settings _settings;
        public JwtHelper(ILogger<SqlHelper> logger, Settings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        public string GetJwtToken(UserJWTClaim user)
        {
            var serializeUser = JsonConvert.SerializeObject(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.UserData, serializeUser),
            };

            var JWTSecretToken = Environment.GetEnvironmentVariable("JWTSecretToken");
            var JWTDomainName = Environment.GetEnvironmentVariable("JWTDomainName");
            
            var key = Encoding.ASCII.GetBytes(JWTSecretToken);
            var securityKey = new SymmetricSecurityKey(key);

            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(JWTDomainName, JWTDomainName, claims, expires: DateTime.Now.AddMinutes(180), signingCredentials: creds);
            var encodedString = System.Text.Encoding.UTF8.GetBytes(new JwtSecurityTokenHandler().WriteToken(token));
            var base64Token = System.Convert.ToBase64String(encodedString);
            return base64Token;
        }

        public bool ValidateJWTToken(string jwtToken)
        {
            var JWTSecretToken = Environment.GetEnvironmentVariable("JWTSecretToken");
            var JWTDomainName = Environment.GetEnvironmentVariable("JWTDomainName");
            var key = Encoding.ASCII.GetBytes(JWTSecretToken);

            try {
                new JwtSecurityTokenHandler().ValidateToken(jwtToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = JWTDomainName,
                    ValidAudience = JWTDomainName
                }, out SecurityToken validatedToken);

                var validJwt = validatedToken as JwtSecurityToken;
                var userClaim = validJwt.Claims.First(claim => claim.Type == ClaimTypes.UserData).Value;
                var testDeserialize = JsonConvert.DeserializeObject<UserRegistration>(userClaim);

                return validatedToken != null;
            } catch (Exception ex){

                return false;

            }
            
        }

        public string GetTokenInfo(string jwtToken)
        {
            var JWTSecretToken = Environment.GetEnvironmentVariable("JWTSecretToken");
            var JWTDomainName = Environment.GetEnvironmentVariable("JWTDomainName");
            var key = Encoding.ASCII.GetBytes(JWTSecretToken);

            try
            {
                new JwtSecurityTokenHandler().ValidateToken(jwtToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = JWTDomainName,
                    ValidAudience = JWTDomainName
                }, out SecurityToken validatedToken);

                var validJwt = validatedToken as JwtSecurityToken;
                
                var claimInfo = validJwt.Claims.First(claim => claim.Type == ClaimTypes.UserData).Value;
                return claimInfo;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public SessionValid verifyTokenSession(string authHeader)
        {
            SessionValid heartBeat = new SessionValid { valid = false };
            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                string encodedToken = authHeader.Substring("Bearer ".Length).Trim();
                byte[] data = Convert.FromBase64String(encodedToken);
                string jwtToken = System.Text.Encoding.UTF8.GetString(data);
                heartBeat = new SessionValid { valid = ValidateJWTToken(jwtToken), token = jwtToken };
            }

            return heartBeat;
        }

    }
}
