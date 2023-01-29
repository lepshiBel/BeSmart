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

        public async Task<StatusTheme> CheckIfThemeIsCompleted(StatusLesson updated)
        {
            var statusTheme = repositoryManger.StatusTheme.CheckIfThemeIsPassed(updated.StatusThemeId);

            if  (statusTheme is null) return null;

            return await repositoryManger.StatusTheme.UpdateStatusTheme(statusTheme.Id, "Пройдено");
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
