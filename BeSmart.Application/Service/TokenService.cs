using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.User;
using BeSmart.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BeSmart.Application.Service
{
    public class TokenService : ITokenService
    {
        private readonly IUserService userService;
        public TokenService(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<UserLoginResponseDTO> Authenticate(UserLoginRequestDTO userDto)
        {
            var user = await userService.FindUserByNameAsync(userDto);

            if (user == null) return null;

            var token = GenerateToken(user);
            var response = new UserLoginResponseDTO(user, token);
            return response;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = SomeOptions.GenerateBytes();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = SomeOptions.Issuer,
                Audience = SomeOptions.Audience,

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

                Subject = new ClaimsIdentity(new Claim[]
                {
                                new Claim("id", user.Id.ToString()),
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
