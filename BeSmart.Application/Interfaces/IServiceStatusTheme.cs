using BeSmart.Domain.DTOs.Answer;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Interfaces
{
    public interface IServiceStatusTheme
    {
        Task<StatusTheme> AddStatusThemeAsync(int themeId, int membershipId);
        Task<StatusTheme> StartNewThemeAsync(StatusTheme existed);
        Task<StatusTheme>? CheckIfThemeStarted(int statusThemeId);
    }
}
