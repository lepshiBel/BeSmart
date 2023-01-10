using BeSmart.Domain.DTOs.Course;
using BeSmart.Domain.DTOs.Lesson;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Interfaces
{
    public interface IServiceCourse
    {
        Task<List<CourseDTO>> GetAllCoursesAsync();
        Task<CourseDTO> FindCourseByIdAsync(int id);
        Task<CourseWithThemesDTO> GetCourseWithThemesAsync(int id);
        Task<CourseDTO> AddCourseAsync(CourseCreationDTO courseCreationDto);
        Task<CourseDTO> UpdateCourseAsync(int id, CourseUpdateDTO courseDto);
        Task<Course> DeleteCourseAsync(int id);
    }
}
