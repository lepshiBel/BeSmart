using BeSmart.Application.Interfaces;
using BeSmart.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeSmart.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusLessonController : Controller
    {
        private readonly IServiceStatusLesson statusLessonService;

        public StatusLessonController(IServiceStatusLesson statusLessonService)
        {
            this.statusLessonService = statusLessonService;
        }   

        //[Authorize(Roles ="user, admin")]
        [HttpPut("PassTheLesson/{statusLessonId}")]
        public async Task<ActionResult<StatusLesson>> PassTheLesson(int statusLessonId) //
        {
            var updated = await statusLessonService.PassTheLesson(statusLessonId);

            if (updated is null)
            {
                return BadRequest("Passed statusLesson id is invalid or you have already completed this lesson");
            }

            var updatedStatusTheme = await statusLessonService.CheckIfThemeIsCompleted(updated);

            return updatedStatusTheme is null ?
                Ok("Lesson marked as completed") :
                Ok("The lesson that you have passed now is last in current theme. Current theme marked as comleted");
        }
    }
}
