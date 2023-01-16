using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.Card;
using BeSmart.Domain.DTOs.User;
using BeSmart.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeSmart.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ITokenService tokenService;
        private readonly IUserService userService;

        public AccountController(ITokenService tokenService, IUserService userService)
        {
            this.tokenService = tokenService;
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public IActionResult Login([FromBody] UserLoginRequestDTO user)
        {
            var authRes = tokenService.Authenticate(user);

            return authRes.Result == null ? Unauthorized("Entered userName or password is invalid") : Ok(authRes.Result);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("Update/admin/{id}")]
        public async Task<ActionResult<User>> UpdateByAdmin(int id, [FromBody] User user)
        {
            var updated = await userService.UpdateUserByAdminAsync(id, user);

            if (updated is null)
            {
                return BadRequest("User object is invalid");
            }

            return RedirectToAction("Get", "Users", updated.Id);
        }

        [Authorize(Roles = "user")]
        [HttpPut("Update/user/{id}")]
        public async Task<ActionResult<User>> UpdateByUser(int id, [FromBody] UserLoginRequestDTO user)
        {
            var updated = await userService.UpdateUserByUserAsync(id, user);

            if (updated is null)
            {
                return BadRequest("User object is invalid");
            }

            return RedirectToAction("Get", "Users", updated.Id);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await userService.DeleteUserAsync(id);

            if (entity == null)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
