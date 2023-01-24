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

        // if (count кол-во записей в таблице statusLesson с statusThemeId == theme.
        // countlessons то статус темы меняется на пройдено и все записи из таблицы status lesson удаляются)
        public async Task<StatusTheme> CheckIfThemeIsCompleted(StatusLesson updated)
        {
            var statusThemeToDelete = repositoryManger.StatusLesson.CheckIfThemeIsPassed(updated);

            if  (statusThemeToDelete is null) return null;

            return await repositoryManger.StatusTheme.UpdateStatusTheme(Convert.ToInt32(statusThemeToDelete), "Пройдено");
        }

        public async Task<StatusLesson> PassTheLesson(int statusLessonId)
        {
            var statusLesson = await repositoryManger.StatusLesson.GetAsync(statusLessonId);

            if(statusLesson == null || statusLesson.Status == "Пройден") return null;

            var updated = await repositoryManger.StatusLesson.UpdateStatusAsync(statusLesson, "Пройден");
            return updated;
        }
    }
}
