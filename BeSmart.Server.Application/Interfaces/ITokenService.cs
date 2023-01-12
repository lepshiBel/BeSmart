using BeSmart.Server.Domain.DTOs;
using BeSmart.Server.Domain.Models;

namespace BeSmart.Server.Application.Interfaces
{
    public interface ITokenService
    {
        UserLoginResponseDTO Authenticate(User user);
        public string GenerateToken(User user);
    }
}
