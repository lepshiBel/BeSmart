using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.Card;
using BeSmart.Domain.DTOs.User;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BeSmart.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager repoManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public UserService(IRepositoryManager repoManager, ITokenService tokenService, IMapper mapper)
        {
            this.repoManager = repoManager;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }

        public async Task<UserLoginResponseDTO> RegisterUserAsync(string googleToken, string password)
        {
            var payload = await tokenService.GoogleTokenValidateAsync(googleToken);
            
            var user = new User(payload.Name, payload.Email);

            var hmac = new HMACSHA512();

            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            user.PasswordSalt = hmac.Key;

            await repoManager.User.AddAsync(user);

            var token = tokenService.GenerateToken(user);
            var response = new UserLoginResponseDTO(user, token);

            return response;
        }

        public async Task<UserLoginResponseDTO> LoginUserAsync(string googleToken)
        {
            var payload = await tokenService.GoogleTokenValidateAsync(googleToken);

            var user = await FindUserByEmailAsync(payload.Email);

            if (user is null || user.PasswordHash.Length == 0)
            {
                return null;
            }

            var token = tokenService.GenerateToken(user);
            var response = new UserLoginResponseDTO(user, token);

            return response;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await repoManager.User.GetAllAsync();
        }

        public async Task<User> FindUserByIdAsync(int id)
        {
            return await repoManager.User.GetAsync(id);
        }

        public async Task<User> FindUserByEmailAsync(string email)
        {
            return await repoManager.User.GetUserByEmailAsync(email);
        }

        public async Task<User> UpdateUserByAdminAsync(int id, User user)
        {
            return await repoManager.User.UpdateAsync(id, user);
        }

        public async Task<User> UpdateUserByUserAsync(int id, UserLoginRequestDTO userLoginRequestDto)
        {
            var userToUpdate = mapper.Map<User>(userLoginRequestDto);
            var updated = await repoManager.User.UpdateAsync(id, userToUpdate);
            return updated == null ? null : mapper.Map<User>(updated);
        }

        public async Task<User> DeleteUserAsync(int id)
        {
            return await repoManager.User.DeleteAsync(id);
        }

        public int GetCurrentUserId(HttpContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            if (identity == null) return 0;

            var userClaims = identity.Claims;
            var userId = Convert.ToInt32(userClaims.FirstOrDefault(x => x.Type == "id")?.Value);
            return userId;
        }
    }

}
