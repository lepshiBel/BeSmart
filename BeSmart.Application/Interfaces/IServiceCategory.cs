using BeSmart.Domain.DTOs.Category;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Interfaces
{
    public interface IServiceCategory
    {
        Task<List<CategoryDTO>>? GetAllCategoriesAsync();
        Task<CategoryDTO>? FindCategoryByIdAsync(int id);
        Task<CategoryDTO>? AddCategoryAsync(CategoryCreationDTO categoryDto);
    }
}
