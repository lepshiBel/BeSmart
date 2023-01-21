using BeSmart.Domain.Models;

namespace BeSmart.Domain.Interfaces
{
    public interface IStatusLessonRepository : IRepositoryBase<StatusLesson>
    {
        public Task<StatusLesson> AddStatusLessonAsync(int lessonId, int statusThemeId);
    }
}
