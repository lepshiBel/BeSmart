using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.Theme;
using Microsoft.AspNetCore.Mvc;

namespace BeSmart.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThemeController : ControllerBase
    {
        private readonly IServiceTheme serviceTheme;

        public ThemeController(IServiceTheme serviceTheme)
        {
            this.serviceTheme = serviceTheme;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<ThemeDTO>>> GetAll()
        {
            var themes = await serviceTheme.GetAllThemesAsync();

            if (!themes.Any())
            {
                return NoContent();
            }

            return Ok(themes);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ThemeDTO>> Get(int id)
        {
            var themeDTO = await serviceTheme.FindThemeByIdAsync(id);

            if (themeDTO is null)
            {
                return NoContent();
            }

            return Ok(themeDTO);
        }

        [AllowAnonymous]
        [HttpGet("withLessons/{id}")]
        public async Task<ActionResult<ThemeWithLessonsDTO>> GetThemeWithLessons(int id)
        {
            var themeWithLessonsDto = await serviceTheme.GetThemeWithLessonsAsync(id);

            if (themeWithLessonsDto is null)
            {
                return BadRequest("Passed id is invalid");
            }

            return Ok(themeWithLessonsDto);
        }

        [AllowAnonymous]
        [HttpGet("withTests/{id}")]
        public async Task<ActionResult<ThemeWithTestsDTO>> GetThemeWithTests(int id)
        {
            var themeWithTestsDto = await serviceTheme.GetThemeWithTestsAsync(id);

            if (themeWithTestsDto is null)
            {
                return BadRequest("Passed id is invalid");
            }

            return Ok(themeWithTestsDto);
        }

        [AllowAnonymous]
        [HttpPost("Create/{CourseId}")]
        public async Task<ActionResult> Post(int courseId, [FromBody] ThemeCreationDTO themeCreationDto)
        {
            themeCreationDto.CourseId = courseId;

            var createdTheme = await serviceTheme.AddThemeAsync(themeCreationDto);

            if (createdTheme is null)
            {
                return BadRequest("Theme object is invalid");
            }

            return Ok(createdTheme);
        }

        [AllowAnonymous]
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<ThemeDTO>> Update(int id, [FromBody] ThemeCreationDTO themeUpdateDto)
        {
            var updatedTheme = await serviceTheme.UpdateThemeAsync(id, themeUpdateDto);

            if (updatedTheme is null)
            {
                return BadRequest("Theme object is invalid");
            }

            return Ok(updatedTheme);
        }

        [AllowAnonymous]
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await serviceTheme.DeleteThemeAsync(id);

            if (entity == null)
            {
                return BadRequest("Passed id is invalid");
            }

            return Ok();
        }
    }
}
