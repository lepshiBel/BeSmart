using BeSmart.Server.Application.Interfaces;
using BeSmart.Server.Domain.Models;
using BeSmart.Server.Persistence;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BeSmart.Server.Application.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = SomeOptions.GenerateBytes();

            var tokenDescriptor = new SecurityTokenDescriptor // класс для создания всех атрибутов, передаваемых в токене
            {
                Issuer = SomeOptions.Issuer,
                Audience = SomeOptions.Audience,

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                // SigningCredentials - для определения ключа и алгоритма шифрования
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, user.Username.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                }),

                Expires = DateTime.UtcNow.AddHours(1),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
