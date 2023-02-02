using BeSmart.Domain.DTOs.Answer;
using BeSmart.Domain.DTOs.StatusTheme;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Interfaces
{
    public interface IServiceStatusTheme
    {
        Task<StatusTheme> StartNewThemeAsync(StatusTheme existed);
        Task<StatusTheme>? CheckIfThemeStarted(int statusThemeId);
        Task<StatusThemeWithLessonsDTO> GetStatusThemeWithStatusLessons(int statusThemeId);
    }
}
