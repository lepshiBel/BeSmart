using BeSmart.Application.Interfaces;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using FluentValidation;

namespace BeSmart.Application.Service
{
    public class CategoryService : IServiceCategory
    {
        private readonly IRepositoryManager repoManager;
        private readonly IValidator<Category> validator;
        public CategoryService(IRepositoryManager repoManager, IValidator<Category> validator)
        {
            this.repoManager = repoManager;
            this.validator = validator;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await repoManager.Category.GetAllAsync();
        }

        public async Task<Category> FindCategoryByIdAsync(int id)
        {
            return await repoManager.Category.GetAsync(id);
        }

        public async Task<Category> AddCategoryAsync(Category entity)
        {
            return await repoManager.Category.AddAsync(entity);
        }
    }
}
