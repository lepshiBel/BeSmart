using BeSmart.Domain.Models;

namespace BeSmart.Domain.Interfaces
{
    public interface IStatusThemeRepository : IRepositoryBase<StatusTheme>
    {
        public Task<StatusTheme> AddStatusTheme(int themeId, int membershipId);
        //Task<StatusTheme> GetStatusThemeWithLessonsAndTestsAsync(int statusThemeId);
        public Task<StatusTheme> UpdateStatusTheme(StatusTheme statusTheme, string newStatus);
        public Task<StatusThemeWithLessons> GetStatusThemeWithStatusLessonsWithLessonsAsync(int statusThemeId);
    }
}
