using BeSmart.Application.Interfaces;
using BeSmart.Domain;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;
using Microsoft.EntityFrameworkCore;

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

            // todo 
            //var statusTest = new StatusTest() { TestId = testId, StatusThemeId = statusThemeId };
            //var created = await base.AddAsync(statusTest);
            //return created;
        }

        public async Task<StatusLesson> UpdateStatusAsync(StatusLesson statusLesson, string status)
        {
            statusLesson.Status = status;
            context.StatusLessons.Update(statusLesson);
            await context.SaveChangesAsync();
            return statusLesson;
        }
    }
}
