using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.Course;
using BeSmart.Domain.DTOs.Question;
using Microsoft.AspNetCore.Mvc;

namespace BeSmart.WebApi.Controllers
{
    public class CourseController : ControllerBase
    {
        private readonly IServiceCourse serviceCourse;

        public CourseController(IServiceCourse serviceCourse)
        {
            this.serviceCourse = serviceCourse;
        }

        [HttpGet]
        public async Task<ActionResult<List<CourseDTO>>> GetAll()
        {
            var courses = await serviceCourse.GetAllCoursesAsync();

            if (!courses.Any())
            {
                return NoContent();
            }

            return Ok(courses.OrderBy(a => a.Id));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> Get(int id)
        {
            var courseDto = await serviceCourse.FindCourseByIdAsync(id);

            if (courseDto is null)
            {
                return NoContent();
            }

            return Ok(courseDto);
        }

        [HttpGet("withThemes/{id}")]
        public async Task<ActionResult<CourseWithThemesDTO>> GetCourseWithThemes(int id)
        {
            var coursesWithThemesDto = await serviceCourse.GetCourseWithThemesAsync(id);

            if (coursesWithThemesDto is null)
            {
                return NoContent();
            }

            return Ok(coursesWithThemesDto);
        }

        [HttpPost("Create/{CreatedById}, {CategoryId}")]
        public async Task<ActionResult> Post(int createdById, int categoryId,[FromBody] CourseCreationDTO courseCreationDto)
        {
            courseCreationDto.CreatedById = createdById;
            courseCreationDto.CategoryId = categoryId;

            var createdCourse = await serviceCourse.AddCourseAsync(courseCreationDto);

            if (createdCourse is null)
            {
                return BadRequest("Course object is invalid");
            }

            return Ok(createdCourse);
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult<CourseDTO>> Update(int id, [FromBody] CourseUpdateDTO courseUpdateDTO)
        {
            var updatedCourse = await serviceCourse.UpdateCourseAsync(id, courseUpdateDTO);

            if (updatedCourse is null)
            {
                return BadRequest("Course object is invalid");
            }

            return Ok(updatedCourse);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await serviceCourse.DeleteCourseAsync(id);

            if (entity == null)
            {
                return NoContent();
            }

            return Ok();
        }
    }
}
