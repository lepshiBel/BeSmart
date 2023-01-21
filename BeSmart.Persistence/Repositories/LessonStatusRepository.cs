using BeSmart.Domain;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;

namespace BeSmart.Persistence.Repositories
{
    public class LessonStatusRepository : RepositoryBase<StatusLesson, BeSmartDbContext>, IStatusLessonRepository
    {
        public LessonStatusRepository(BeSmartDbContext context) : base(context) { }

        public async Task<StatusLesson> AddStatusLessonAsync(int lessonId, int statusThemeId)
        {
            var statusLesson = new StatusLesson() { LessonId = lessonId, StatusThemeId = statusThemeId };
            await context.StatusLessons.AddAsync(statusLesson);
            await context.SaveChangesAsync();
            return statusLesson;
        }
    }
}
