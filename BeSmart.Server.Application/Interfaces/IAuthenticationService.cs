using BeSmart.Server.Domain.Models;

namespace BeSmart.Server.Application.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
    }
}
