using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.Lesson;
using BeSmart.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeSmart.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly IServiceLesson serviceLesson;

        public LessonController(IServiceLesson serviceLesson)
        {
            this.serviceLesson = serviceLesson;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<LessonDTO>>> GetAll()
        {
            var lessonsDto = await serviceLesson.GetAllLessonsAsync();

            if (!lessonsDto.Any())
            {
                return NoContent();
            }

            return Ok(lessonsDto.OrderBy(a => a.Id));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<LessonDTO>> Get(int id)
        {
            var lessonDto = await serviceLesson.FindLessonByIdAsync(id);

            if (lessonDto is null)
            {
                return NoContent();
            }

            return Ok(lessonDto);
        }

        [AllowAnonymous]
        [HttpGet("withCards/{id}")]
        public async Task<ActionResult<LessonWithCardsDTO>> GetLessonWithCards(int id)
        {
            var lessonWithCardsDto = await serviceLesson.GetLessonWithCardsAsync(id);

            if (lessonWithCardsDto is null)
            {
                return NoContent();
            }

            return Ok(lessonWithCardsDto);
        }

        [AllowAnonymous]
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<Lesson>> Update(int id, LessonCreationDTO lessonDto)
        {
            var updated = await serviceLesson.UpdateLessonAsync(id, lessonDto);

            if (updated is null)
            {
                return BadRequest("Lesson object is invalid");
            }

            return Ok(updated);
        }

        [AllowAnonymous]
        [HttpPost("Create/{themeId}")]
        public async Task<ActionResult<Lesson>> Post(int themeId, LessonCreationDTO lessonCreationDto)
        {
            lessonCreationDto.ThemeId = themeId;
            var createdLesson = await serviceLesson.AddLessonAsync(lessonCreationDto);

            if (createdLesson is null)
            {
                return BadRequest("Lesson object is invalid");
            }

            return Ok(createdLesson);
        }

        [AllowAnonymous]
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await serviceLesson.DeleteLessonAsync(id);

            if (entity == null)
            {
                return NoContent();
            }

            return Ok();
        }
    }
}
