using BeSmart.Application.Interfaces;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using Microsoft.Extensions.Configuration;

namespace BeSmart.Application.Service
{
    public class StatusLessonService : IServiceStatusLesson
    {
        private readonly IRepositoryManager repositoryManger;

        public StatusLessonService(IRepositoryManager repositoryManger)
        {
            this.repositoryManger = repositoryManger;
        }

        public async Task<StatusLesson> AddStatusLessonAsync(int lessonId, int statusThemeId)
        {
            var createdStatus = await repositoryManger.StatusLesson.AddStatusLessonAsync(lessonId, statusThemeId);
            return createdStatus;
        }

        // проверка если тема пройдена статус темы обновлен на пройдено, удаляются записи об уроках и тестах, пройденных в этой тебе и кол-во пройженных тем в membership++
        public async Task<StatusTheme> CheckIfThemeIsCompleted(StatusLesson updated)
        {
            var statusTheme = await repositoryManger.StatusTheme.CheckIfThemeIsPassed(updated.StatusThemeId);

            if  (statusTheme is null) 
                return null;

            await repositoryManger.StatusLesson.FindStatusLessonsAndDeleteArrange(statusTheme.Id);// удаляются записи об уроках и тестах
            await repositoryManger.StatusTest.FindStatusTestsAndDeleteArrange(statusTheme.Id);
            var updatedStatusTheme = await repositoryManger.StatusTheme.UpdateStatusTheme(statusTheme.Id, "Пройдено");// статус темы обновлен на пройдено
            await repositoryManger.Membership.UpdateAmountOfCompletedThemesAsync(statusTheme.MembershipId); // membersip.AmountOfPassedThemes++
            return updatedStatusTheme;

        }

        // изменение статуса урока на пройден и увеличивает кол-во пройденых уроков в статус_тема
        public async Task<StatusLesson> PassTheLesson(int statusLessonId)
        {
            var statusLesson = await repositoryManger.StatusLesson.GetAsync(statusLessonId);

            if(statusLesson == null || statusLesson.Status == "Пройден") return null;

            var updated = await repositoryManger.StatusLesson.UpdateStatusAsync(statusLesson, "Пройден");
            await repositoryManger.StatusTheme.UpdateAmountOfPassedLessons(statusLesson.StatusThemeId);
            return updated;
        }
    }
}
