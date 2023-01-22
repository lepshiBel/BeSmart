using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.User;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
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

        public async Task<UserLoginResponseDTO> RegisterUserAsync(UserLoginRequestDTO userDto)
        {

            var user = mapper.Map<User>(userDto);

            if (user is null)
            {
                return null;
            }

            var hmac = new HMACSHA512();

            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password));
            user.PasswordSalt = hmac.Key;

            await repoManager.User.AddAsync(user);

            var token = tokenService.GenerateToken(user);
            var response = new UserLoginResponseDTO(user, token);

            return response;
        }

        public async Task<UserLoginResponseDTO> LoginUserAsync(UserLoginRequestDTO userDto, string googleTokenUrl)
        {
            var payload = await tokenService.GoogleTokenValidateAsync(googleTokenUrl);

            var user = await FindUserByNameAsync(userDto);

            if (user is null)
            {
                return null;
            }

            var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password));

            for (var i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return null;
                }
            }

            if (user.Email == payload.Email)
            {
                var token = tokenService.GenerateToken(user);
                var response = new UserLoginResponseDTO(user, token);

                return response;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await repoManager.User.GetAllAsync();
        }

        public async Task<User> FindUserByIdAsync(int id)
        {
            return await repoManager.User.GetAsync(id);
        }

        public async Task<User> FindUserByNameAsync(UserLoginRequestDTO userDto)
        {
            return await repoManager.User.GetUserByNameAsync(userDto.Username, userDto.Email);
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
    }

}
