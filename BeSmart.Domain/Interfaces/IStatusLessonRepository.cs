using BeSmart.Domain.Models;

namespace BeSmart.Domain.Interfaces
{
    public interface IStatusLessonRepository : IRepositoryBase<StatusLesson>
    {
        public Task<StatusLesson> AddStatusLessonAsync(int lessonId, int statusThemeId);

        public Task<StatusLesson> UpdateStatusAsync(StatusLesson statusLesson, string status);
        public Task<int> CheckIfThemeIsPassed(StatusLesson updated);
    }
}
