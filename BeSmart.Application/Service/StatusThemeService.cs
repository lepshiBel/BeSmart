using BeSmart.Application.Interfaces;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Service
{
    public class StatusThemeService : IServiceStatusTheme
    {
        private readonly IRepositoryManager repositoryManger;
        private readonly IServiceStatusLesson serviceStatusLesson;
        private readonly IServiceStatusTest serviceStatusTest;

        public StatusThemeService(IRepositoryManager repositoryManger, IServiceStatusLesson serviceStatusLesson, IServiceStatusTest serviceStatusTest)
        {
            this.repositoryManger = repositoryManger;
            this.serviceStatusLesson = serviceStatusLesson;
            this.serviceStatusTest = serviceStatusTest;
        }

        public async Task<StatusTheme> AddStatusThemeAsync(int themeId, int membershipId)
        {
            var createdStatus = await repositoryManger.StatusTheme.AddStatusTheme(themeId, membershipId);
            return createdStatus;
        }
        public async Task<StatusTheme>? CheckIfThemeStarted(int statusThemeId)
        {
            var statusTheme = await repositoryManger.StatusTheme.GetAsync(statusThemeId);

            if (statusTheme == null) return null;

            return statusTheme.Status == "В процессе" ? null : statusTheme;
        }

        public async Task<StatusTheme> StartNewThemeAsync(StatusTheme existed)
        {
            var updated = await repositoryManger.StatusTheme.UpdateStatusTheme(existed, "В процессе");

            var themeWithLessons = await repositoryManger.Theme.GetThemeWithLessonsAsync(updated.ThemeId); // TODO сделать одним методом
            var themeWithTests = await repositoryManger.Theme.GetThemeWithTestsAsync(updated.ThemeId);


            foreach (var lesson in themeWithLessons.Lessons)
            {
                await serviceStatusLesson.AddStatusLessonAsync(lesson.Id, updated.Id);
            }

            foreach (var test in themeWithTests.Tests)
            {
                await serviceStatusTest.AddStatusTestAsync(test.Id, updated.Id);
            }

            return updated;
        }
    }
}
