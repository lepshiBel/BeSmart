using BeSmart.Server.Domain.Models;

namespace BeSmart.Server.Application.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
