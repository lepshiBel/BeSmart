using BeSmart.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeSmart.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusThemeController : ControllerBase
    {
        private readonly IServiceStatusTheme serviceStatusTheme;
        public StatusThemeController(IServiceStatusTheme serviceStatusTheme)
        {
            this.serviceStatusTheme = serviceStatusTheme;
        }

        //[Authorize(Roles ="user, admin")]
        [HttpPost("StartNewTheme/{statusThemeId}")]
        public async Task<ActionResult> StartNewTheme(int statusThemeId) 
        {
            var existed = await serviceStatusTheme.CheckIfThemeStarted(statusThemeId);

            if (existed is null) return Ok("You already have passed this theme");

            var updatedTheme = await serviceStatusTheme.StartNewThemeAsync(existed);

            if (updatedTheme is null) return BadRequest("Passed statusThemeId is not valid");

            return Ok(updatedTheme);
        }

        //[Authorize(Roles ="user, admin")]
        [HttpGet("GetStatusThemeWithLessons/{statusThemeId}")]
        public async Task<ActionResult> GetStatusThemeWithLessons(int statusThemeId) // TODO проверка на валидность statusThemeId
        {
            var existed = await serviceStatusTheme.CheckIfThemeStarted(statusThemeId);

            if (existed != null) return Ok("You have'n started this theme");

            var statusTheme = await serviceStatusTheme.GetStatusThemeWithStatusLessons(statusThemeId);

            if (statusTheme is null) return BadRequest("Passed statusThemeId is not valid");

            return Ok(statusTheme);
        }
    }
}
