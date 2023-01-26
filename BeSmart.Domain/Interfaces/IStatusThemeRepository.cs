using BeSmart.Domain.Models;

namespace BeSmart.Domain.Interfaces
{
    public interface IStatusThemeRepository : IRepositoryBase<StatusTheme>
    {
        public Task<StatusTheme> AddStatusTheme(int themeId, int membershipId);
        public Task<StatusTheme> UpdateStatusTheme(int statusThemeId, string newStatus);
        public Task<StatusTheme> UpdateAmountOfPassedLessons(int statusThemeId);
        public Task<StatusThemeWithLessons> GetStatusThemeWithStatusLessonsWithLessonsAsync(int statusThemeId);
        public Task<StatusTheme> CheckIfThemeIsPassed(int statusThemeId);
    }
}
