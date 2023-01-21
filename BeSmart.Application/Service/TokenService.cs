using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.User;
using BeSmart.Domain.Models;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BeSmart.Application.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public TokenService(IConfiguration configuration, IUserService userService, IMapper mapper)
        {
            this.configuration = configuration;
            this.userService = userService;
            this.mapper = mapper;
        }

        public async Task<GoogleJsonWebSignature.Payload> GoogleTokenValidateAsync(string tokenUrl)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new List<string>
                {
                    configuration["Authentication:Google:ClientId"]
                }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(tokenUrl, settings);

            return payload;
        }

        public async Task<UserLoginResponseDTO> Authenticate(UserLoginRequestDTO userDto, string googleTokenUrl)
        {
            var payload = await GoogleTokenValidateAsync(googleTokenUrl);
            
            var user = await userService.FindUserByNameAsync(userDto);

            if (user.Email == payload.Email)
            {
                var token = GenerateToken(user);
                var response = new UserLoginResponseDTO(user, token);

                return response;
            }
            else
            {
                return null;
            }            
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
