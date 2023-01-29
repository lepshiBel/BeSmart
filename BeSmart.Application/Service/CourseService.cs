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
        private readonly ICacheService _cacheService;

        public CourseService(IRepositoryManager repoManager, IMapper mapper, ICacheService cacheService)
        {
            this.repoManager = repoManager;
            this.mapper = mapper;
            _cacheService = cacheService;
        }

        private string GetCacheKey(string dataId)
        {
            return _cacheService.GetKey(nameof(CourseService), dataId);
        }

        public async Task<List<CourseDTO>> GetAllCoursesAsync()
        {
            var cacheKey = GetCacheKey("all");
            var cachedData = await _cacheService.GetCachedDataAsync<List<CourseDTO>>(cacheKey);

            if (cachedData != null)
            {
                return cachedData;
            }
            
            var courses = await repoManager.Course.GetAllAsync();
            var mappedResult = courses == null ? null : mapper.Map<List<CourseDTO>>(courses);
            
            return mappedResult != null
                ? await _cacheService.CacheDataAsync(cacheKey, mappedResult)
                : mappedResult;
        }

        public async Task<CourseDTO> FindCourseByIdAsync(int id)
        {
            var cacheKey = GetCacheKey(id.ToString());
            var cachedData = await _cacheService.GetCachedDataAsync<CourseDTO>(cacheKey);

            if (cachedData != null)
            {
                return cachedData;
            }
            
            var course = await repoManager.Course.GetAsync(id);
            var mappedResult = course == null ? null : mapper.Map<CourseDTO>(course);
            
            return mappedResult != null
                ? await _cacheService.CacheDataAsync(cacheKey, mappedResult)
                : mappedResult;
        }

        public async Task<CourseDTO> AddCourseAsync(CourseCreationDTO courseCreationDto)
        {
            var courseToCreate = mapper.Map<Course>(courseCreationDto);
            var createdCourse = await repoManager.Course.AddAsync(courseToCreate);

            return mapper.Map<CourseDTO>(createdCourse);
        }

        public async Task<Course> DeleteCourseAsync(int id)
        {
            var result = await repoManager.Course.DeleteAsync(id);

            var cacheKey = GetCacheKey(id.ToString());
            await _cacheService.DeleteCachedData(cacheKey);

            return result;
        }

        public async Task<CourseWithThemesDTO> GetCourseWithThemesAsync(int id)
        {
            var cacheKey = GetCacheKey($"-with-themes-{id.ToString()}");
            var cachedData = await _cacheService.GetCachedDataAsync<CourseWithThemesDTO>(cacheKey);

            if (cachedData != null)
            {
                return cachedData;
            }
            
            var courseWithThemes = await repoManager.Course.GetCourseWithThemesAsync(id);
            var mappedResult = courseWithThemes == null ? null : mapper.Map<CourseWithThemesDTO>(courseWithThemes);
            
            return mappedResult != null
                ? await _cacheService.CacheDataAsync(cacheKey, mappedResult)
                : mappedResult;
        }

        public async Task<CourseDTO> UpdateCourseAsync(int id, CourseUpdateDTO courseDto)
        {
            var courseToUpdate = mapper.Map<Course>(courseDto);
            var updatedCourse = await repoManager.Course.UpdateAsync(id, courseToUpdate);

            var mappedResult = updatedCourse == null ? null : mapper.Map<CourseDTO>(updatedCourse);

            var cacheKey = GetCacheKey(id.ToString());
            return mappedResult != null
                ? await _cacheService.CacheDataAsync(cacheKey, mappedResult)
                : mappedResult;
        }
    }
}
