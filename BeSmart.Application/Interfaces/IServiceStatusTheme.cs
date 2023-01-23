using BeSmart.Domain.DTOs.Answer;
using BeSmart.Domain.DTOs.StatusTheme;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Interfaces
{
    public interface IServiceStatusTheme
    {
        Task<StatusTheme> AddStatusThemeAsync(int themeId, int membershipId);
        Task<StatusTheme> StartNewThemeAsync(StatusTheme existed);
        Task<StatusTheme>? CheckIfThemeStarted(int statusThemeId);
        Task<StatusThemeWithLessonsDTO> GetStatusThemeWithStatusLessons(int statusThemeId);
        Task<StatusTheme> UpdateStatus(int statusThemeId, string newStatus);

    }
}
