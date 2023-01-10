using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.Course;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Service
{
    public class CourseService : IServiceCourse
    {
        private readonly IRepositoryManager repoManager;
        private readonly IMapper mapper;

        public CourseService(IRepositoryManager repoManager, IMapper mapper)
        {
            this.repoManager = repoManager;
            this.mapper = mapper;
        }

        public async Task<List<CourseDTO>> GetAllCoursesAsync()
        {
            var courses = await repoManager.Course.GetAllAsync();

            return courses == null ? null : mapper.Map<List<CourseDTO>>(courses);
        }

        public async Task<CourseDTO> FindCourseByIdAsync(int id)
        {
            var course = await repoManager.Course.GetAsync(id);

            return course == null ? null : mapper.Map<CourseDTO>(course);
        }

        public async Task<CourseDTO> AddCourseAsync(CourseCreationDTO courseCreationDto)
        {
            var courseToCreate = mapper.Map<Course>(courseCreationDto);
            var createdCourse = await repoManager.Course.AddAsync(courseToCreate);

            return mapper.Map<CourseDTO>(createdCourse);


        }

        public async Task<Course> DeleteCourseAsync(int id)
        {
            return await repoManager.Course.DeleteAsync(id);
        }

        public async Task<CourseWithThemesDTO> GetCourseWithThemesAsync(int id)
        {
            var courseWithThemes = await repoManager.Course.GetCourseWithThemesAsync(id);

            return courseWithThemes == null ? null : mapper.Map<CourseWithThemesDTO>(courseWithThemes);
        }

        public async Task<CourseDTO> UpdateCourseAsync(int id, CourseUpdateDTO courseDto)
        {
            var courseToUpdate = mapper.Map<Course>(courseDto);
            var updatedCourse = await repoManager.Course.UpdateAsync(id, courseToUpdate);

            return updatedCourse == null ? null : mapper.Map<CourseDTO>(updatedCourse);
        }
    }
}
