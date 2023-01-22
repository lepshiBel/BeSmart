using BeSmart.Application.Interfaces;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;

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
    }
}
