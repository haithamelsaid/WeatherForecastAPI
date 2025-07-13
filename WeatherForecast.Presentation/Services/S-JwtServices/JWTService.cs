using WeatherForecast.Presentation.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WeatherForecast.Presentation.Services.S_JwtServices
{
    public class JWTService(IOptions<JwtTokenSettings> jwtTokenSettings) : IJWTService
    {
        private readonly JwtTokenSettings _jwtTokenSettings = jwtTokenSettings.Value;
        public string GenerateToken(string id)
        {
            JwtSecurityTokenHandler tokenHandler = new();

            byte[] key = Encoding.UTF8.GetBytes(_jwtTokenSettings.SigningKey);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Issuer = _jwtTokenSettings.Issuer,
                Audience = _jwtTokenSettings.Audience,
                Subject = new ClaimsIdentity(new Claim[] {
                    new("Id", id),
                    new(ClaimTypes.Role, "user")
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Expires = DateTime.UtcNow.AddDays(1)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
