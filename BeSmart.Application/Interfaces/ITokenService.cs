using BeSmart.Domain.DTOs.User;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Interfaces
{
    public interface ITokenService
    {
        UserLoginResponseDTO Authenticate(UserLoginRequestDTO user);
        public string GenerateToken(User user);
    }
}
