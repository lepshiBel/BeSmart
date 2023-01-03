using BeSmart.Domain.Models;

namespace BeSmart.Application.Interfaces
{
    public interface IServiceCategory
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> FindCategoryByIdAsync(int id);
        Task<Category> AddCategoryAsync(Category category);
    }
}
