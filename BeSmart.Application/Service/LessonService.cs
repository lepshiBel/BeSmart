using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.Lesson;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Service
{
    public class LessonService : IServiceLesson
    {
        private readonly IRepositoryManager repoManager;
        private readonly IMapper mapper;
        private readonly ICacheService _cacheService;

        public LessonService(IRepositoryManager repoManager, IMapper mapper, ICacheService cacheService)
        {
            this.repoManager = repoManager;
            this.mapper = mapper;
            _cacheService = cacheService;
        }

        private string GetCacheKey(string dataId)
        {
            return _cacheService.GetKey(nameof(LessonService), dataId);
        }
        
        public async Task<List<LessonDTO>> GetAllLessonsAsync()
        {
            var cacheKey = GetCacheKey("all");
            var cachedData = await _cacheService.GetCachedDataAsync<List<LessonDTO>>(cacheKey);

            if (cachedData != null)
            {
                return cachedData;
            }
            
            var lessons = await repoManager.Lesson.GetAllAsync();
            var mappedResult = lessons == null ? null : mapper.Map<List<LessonDTO>>(lessons);
            
            return mappedResult != null
                ? await _cacheService.CacheDataAsync(cacheKey, mappedResult)
                : mappedResult;
        }

        public async Task<LessonDTO> FindLessonByIdAsync(int id)
        {
            var cacheKey = GetCacheKey(id.ToString());
            var cachedData = await _cacheService.GetCachedDataAsync<LessonDTO>(cacheKey);

            if (cachedData != null)
            {
                return cachedData;
            }
            
            var lesson = await repoManager.Lesson.GetAsync(id);
            var mappedResult = lesson == null ? null : mapper.Map<LessonDTO>(lesson);
            
            return mappedResult != null
                ? await _cacheService.CacheDataAsync(cacheKey, mappedResult)
                : mappedResult;
        }

        public async Task<LessonWithCardsDTO> GetLessonWithCardsAsync(int id)
        {
            var cacheKey = GetCacheKey($"-with-cards-{id.ToString()}");
            var cachedData = await _cacheService.GetCachedDataAsync<LessonWithCardsDTO>(cacheKey);

            if (cachedData != null)
            {
                return cachedData;
            }
            
            var lessonWithCards = await repoManager.Lesson.GetLessonWithCardsAsync(id);
            var mappedResult = lessonWithCards == null ? null : mapper.Map<LessonWithCardsDTO>(lessonWithCards);
            
            return mappedResult != null
                ? await _cacheService.CacheDataAsync(cacheKey, mappedResult)
                : mappedResult;
        }

        public async Task<LessonDTO> AddLessonAsync(LessonCreationDTO lessonCreationDto)
        {
            var lessonToCreate = mapper.Map<Lesson>(lessonCreationDto);
            var createdLesson = await repoManager.Lesson.AddAsync(lessonToCreate);
            return createdLesson == null ? null : mapper.Map<LessonDTO>(createdLesson);
        }

        public async Task<LessonDTO> UpdateLessonAsync(int id, LessonDTO lessonDto)
        {
            var lesson = mapper.Map<Lesson>(lessonDto);
            var updated = await repoManager.Lesson.UpdateAsync(id, lesson);
            var mappedResult = updated == null ? null : mapper.Map<LessonDTO>(updated);
            
            var cacheKey = GetCacheKey(id.ToString());
            return mappedResult != null
                ? await _cacheService.CacheDataAsync(cacheKey, mappedResult)
                : mappedResult;
        }

        public async Task<Lesson> DeleteLessonAsync(int id)
        {
            var result = await repoManager.Lesson.DeleteAsync(id);

            var cacheKey = GetCacheKey(id.ToString());
            await _cacheService.DeleteCachedData(cacheKey);

            return result;
        }
    }
}
