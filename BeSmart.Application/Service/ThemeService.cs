using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.Theme;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Service
{
    public class ThemeService : IServiceTheme
    {
        private readonly IRepositoryManager repoManager;
        private readonly IMapper mapper;
        private readonly ICacheService _cacheService;

        public ThemeService(IRepositoryManager repoManager, IMapper mapper, ICacheService cacheService)
        {
            this.repoManager = repoManager;
            this.mapper = mapper;
            _cacheService = cacheService;
        }
        
        private string GetCacheKey(string dataId)
        {
            return _cacheService.GetKey(nameof(ThemeService), dataId);
        }
        
        public async Task<List<ThemeDTO>> GetAllThemesAsync()
        {
            var cacheKey = GetCacheKey("all");
            var cachedData = await _cacheService.GetCachedDataAsync<List<ThemeDTO>>(cacheKey);

            if (cachedData != null)
            {
                return cachedData;
            }
            
            var themes = await repoManager.Theme.GetAllAsync();
            var mappedResult = themes == null ? null : mapper.Map<List<ThemeDTO>>(themes);
            
            return mappedResult != null
                ? await _cacheService.CacheDataAsync(cacheKey, mappedResult)
                : mappedResult;
        }
        public Task<ThemeDTO> FindThemeByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<ThemeWithLessonsDTO> GetThemeWithLessonsAsync(int id)
        {
            var cacheKey = GetCacheKey($"-with-lessons-{id.ToString()}");
            var cachedData = await _cacheService.GetCachedDataAsync<ThemeWithLessonsDTO>(cacheKey);

            if (cachedData != null)
            {
                return cachedData;
            }
            
            var themesWithLessons = await repoManager.Theme.GetThemeWithLessonsAsync(id);
            var mappedResult = themesWithLessons == null ? null : mapper.Map<ThemeWithLessonsDTO>(themesWithLessons);
            
            return mappedResult != null
                ? await _cacheService.CacheDataAsync(cacheKey, mappedResult)
                : mappedResult;
        }
        public async Task<ThemeWithTestsDTO> GetThemeWithTestsAsync(int id)
        {
            var cacheKey = GetCacheKey($"-with-tests-{id.ToString()}");
            var cachedData = await _cacheService.GetCachedDataAsync<ThemeWithTestsDTO>(cacheKey);

            if (cachedData != null)
            {
                return cachedData;
            }
            
            var themesWithTests = await repoManager.Theme.GetThemeWithTestsAsync(id);
            var mappedResult = themesWithTests == null ? null : mapper.Map<ThemeWithTestsDTO>(themesWithTests);
            
            return mappedResult != null
                ? await _cacheService.CacheDataAsync(cacheKey, mappedResult)
                : mappedResult;
        }

        public async Task<ThemeDTO> AddThemeAsync(ThemeCreationDTO themeCreationDto)
        {
            var theme = mapper.Map<Theme>(themeCreationDto);
            var created = await repoManager.Theme.AddAsync(theme);
            return mapper.Map<ThemeDTO>(created);
        }

        public async Task<ThemeDTO> UpdateThemeAsync(int id, ThemeCreationDTO themeUpdateDto)
        {
            var theme = mapper.Map<Theme>(themeUpdateDto);
            var updated = await repoManager.Theme.UpdateAsync(id, theme);
            var mappedResult = updated == null ? null : mapper.Map<ThemeDTO>(updated);
            
            var cacheKey = GetCacheKey(id.ToString());
            return mappedResult != null
                ? await _cacheService.CacheDataAsync(cacheKey, mappedResult)
                : mappedResult;
        }
        public async Task<Theme> DeleteThemeAsync(int id)
        {
            var result = await repoManager.Theme.DeleteAsync(id);
            
            var cacheKey = GetCacheKey(id.ToString());
            await _cacheService.DeleteCachedData(cacheKey);

            return result;
        }
    }
}
