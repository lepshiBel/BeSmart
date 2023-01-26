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

        public CategoryService(IRepositoryManager repoManager, IMapper mapper)
        {
            this.repoManager = repoManager;
            this.mapper = mapper;
        }

        public async Task<List<CategoryDTO>>? GetAllCategoriesAsync()
        {
            var categories = await repoManager.Category.GetAllAsync();
            return categories == null ? null : mapper.Map<List<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> FindCategoryByIdAsync(int id)
        {
            var category = await repoManager.Category.GetAsync(id);
            return category == null ? null : mapper.Map<CategoryDTO>(category);
        }
        public async Task<CategoryDTO> AddCategoryAsync(CategoryCreationDTO categoryDto)
        {
            var category = mapper.Map<Category>(categoryDto);
            var created = await repoManager.Category.AddAsync(category);
            return mapper.Map<CategoryDTO>(created);
        }
    }
}
