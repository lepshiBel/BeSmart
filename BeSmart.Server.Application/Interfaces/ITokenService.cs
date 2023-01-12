using BeSmart.Server.Domain.Models;

namespace BeSmart.Server.Application.Interfaces
{
    public interface ITokenService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        public string GenerateToken(User user);
    }
}
