using BeSmart.Domain.DTOs.Theme;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Interfaces
{
    public interface IServiceTheme
    {
        Task<List<ThemeDTO>> GetAllThemesAsync();
        Task<ThemeDTO> FindThemeByIdAsync(int id);
        Task<ThemeWithLessonsDTO> GetThemeWithLessonsAsync(int id);
        Task<ThemeWithTestsDTO> GetThemeWithTestsAsync(int id);
        Task<ThemeDTO> AddThemeAsync(ThemeCreationDTO themeCreationDto);
        Task<ThemeDTO> UpdateThemeAsync(int id, ThemeUpdateDTO themeUpdateDto);
        Task<Theme> DeleteThemeAsync(int id);
    }
}
