using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeSmart.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ITokenService tokenService;
        public AuthController(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public IActionResult Login([FromBody] UserLoginRequestDTO user)
        {
            var authRes = tokenService.Authenticate(user);

            return authRes == null ? BadRequest("") : Ok(authRes);
        }
    }
}
