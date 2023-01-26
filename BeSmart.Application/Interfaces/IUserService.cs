using BeSmart.Domain.DTOs.User;
using BeSmart.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace BeSmart.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserLoginResponseDTO> RegisterUserAsync(string googleToken, string password);
        Task<UserLoginResponseDTO> LoginUserAsync(string googleToken);
        Task<List<User>> GetAllUsersAsync();
        Task<User> FindUserByIdAsync(int id);
        Task<User> FindUserByEmailAsync(string email);
        Task<User> UpdateUserByAdminAsync(int id, User user);
        Task<User> UpdateUserByUserAsync(int id, UserLoginRequestDTO user);
        Task<User> DeleteUserAsync(int id);
        int GetCurrentUserId(HttpContext context);
    }
}
