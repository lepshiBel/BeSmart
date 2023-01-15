using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeSmart.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ITokenService tokenService;
        public AccountController(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public IActionResult Login([FromBody] UserLoginRequestDTO user)
        {
            var authRes = tokenService.Authenticate(user);

            return authRes.Result == null ? Unauthorized("Entered userName or password is invalid") : Ok(authRes.Result);
        }
    }
}
