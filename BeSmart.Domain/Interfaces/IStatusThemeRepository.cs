using BeSmart.Domain.Models;

namespace BeSmart.Domain.Interfaces
{
    public interface IStatusThemeRepository : IRepositoryBase<StatusTheme>
    {
        public Task<StatusTheme> AddStatusTheme(int themeId, int membershipId);
        //Task<StatusTheme> GetStatusThemeWithLessonsAndTestsAsync(int statusThemeId);
        public Task<StatusTheme> UpdateStatusTheme(int statusThemeId, string newStatus);
        public Task<StatusThemeWithLessons> GetStatusThemeWithStatusLessonsWithLessonsAsync(int statusThemeId);
    }
}
