using AutoMapper;
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
        private readonly IMapper mapper;

        public TokenService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        private List<User> users = new List<User> {
            new User {
                Id = 1, Username = "test1", Email = "somemail@gmail.com", Password = "12345", Role = "user"
            },
            new User {
                Id = 2, Username = "test2", Email = "somemail@gmail.com", Password = "12345", Role = "user"
            }
        };

        public UserLoginResponseDTO Authenticate(User user)
        {

            var userLoginReqDto = mapper.Map<UserLoginRequestDTO>(user);

            var existionUser = users.SingleOrDefault(x => x.Username == userLoginReqDto.Username && x.Password == userLoginReqDto.Password);

            if (existionUser == null)
            {
                return null;
            }

            var token = GenerateToken(existionUser);

            return new UserLoginResponseDTO(existionUser, token);
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
