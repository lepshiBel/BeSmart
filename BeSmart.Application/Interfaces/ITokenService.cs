using BeSmart.Domain.DTOs.User;
using BeSmart.Domain.Models;
using Google.Apis.Auth;

namespace BeSmart.Application.Interfaces
{
    public interface ITokenService
    {
        Task<GoogleJsonWebSignature.Payload> GoogleTokenValidateAsync(string tokenUrl);
        Task<UserLoginResponseDTO> Authenticate(UserLoginRequestDTO user, string tokenUrl);
        public string GenerateToken(User user);
    }
}
