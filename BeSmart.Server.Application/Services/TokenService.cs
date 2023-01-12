using BeSmart.Server.Application.Interfaces;
using BeSmart.Server.Domain.DTOs;
using BeSmart.Server.Domain.Models;
using BeSmart.Server.Persistence;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BeSmart.Server.Application.Services
{
    public class TokenService : ITokenService
    {
        //private readonly IMapper mapper;

        //public TokenService(IMapper mapper)
        //{
        //    this.mapper = mapper;
        //}

        private List<User> users = new List<User> {
            new User {
                Id = 1, Username = "test1", Email = "somemail@gmail.com", Password = "12345", Role = "user"
            },
            new User {
                Id = 2, Username = "test2", Email = "somemail@gmail.com", Password = "12345", Role = "user"
            }
        };

        public UserLoginResponseDTO Authenticate(UserLoginRequestDTO userDto)
        {
            var user = users.SingleOrDefault(x => x.Username == userDto.Username && x.Password == userDto.Password);

            if (user == null)
            {
                return null;
            }

            var token = GenerateToken(user);

            return new UserLoginResponseDTO(user, token);
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
