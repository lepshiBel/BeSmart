using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.Category;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Service
{
    public class CategoryService : IServiceCategory
    {
        private readonly IRepositoryManager repoManager;
        private readonly IMapper mapper;
        private readonly ICacheService _cacheService;

        public CategoryService(IRepositoryManager repoManager, IMapper mapper, ICacheService cacheService)
        {
            this.repoManager = repoManager;
            this.mapper = mapper;
            _cacheService = cacheService;
        }

        private string GetCacheKey(string dataId)
        {
            return _cacheService.GetKey(nameof(CategoryService), dataId);
        }
        
        public async Task<List<CategoryDTO>>? GetAllCategoriesAsync()
        {
            var cacheKey = GetCacheKey("all");
            var cachedData = await _cacheService.GetCachedDataAsync<List<CategoryDTO>>(cacheKey);

            if (cachedData != null)
            {
                return cachedData;
            }
            
            var categories = await repoManager.Category.GetAllAsync();
            var mappedResult = categories == null ? null : mapper.Map<List<CategoryDTO>>(categories);

            return mappedResult != null
                ? await _cacheService.CacheDataAsync(cacheKey, mappedResult)
                : mappedResult;
        }

        public async Task<CategoryDTO> FindCategoryByIdAsync(int id)
        {
            var cacheKey = GetCacheKey(id.ToString());
            var cachedData = await _cacheService.GetCachedDataAsync<CategoryDTO>(cacheKey);

            if (cachedData != null)
            {
                return cachedData;
            }
            
            var category = await repoManager.Category.GetAsync(id);
            var mappedResult = category == null ? null : mapper.Map<CategoryDTO>(category);
            
            return mappedResult != null
                ? await _cacheService.CacheDataAsync(cacheKey, mappedResult)
                : mappedResult;
        }

        public async Task<CategoryDTO> AddCategoryAsync(CategoryCreationDTO categoryDto)
        {
            var category = mapper.Map<Category>(categoryDto);
            var created = await repoManager.Category.AddAsync(category);
            return mapper.Map<CategoryDTO>(created);
        }
    }
}
