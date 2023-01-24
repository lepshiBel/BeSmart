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
        }

        public async Task<int> CheckIfThemeIsPassed(StatusLesson updated)
        {
            var info = context.StatusLessons.Where(x => x.Id == updated.Id).Include(x => x.StatusTheme)
                .ThenInclude(x => x.Theme)
                .Select(s => new
                {
                    StatusLessonId = s.Id,
                    StatusThemeId = s.StatusThemeId,
                    ThemeId = s.StatusTheme.ThemeId,
                    AmountOfLessonsInTheme = s.StatusTheme.Theme.CountLesson
                }).FirstOrDefault();

            var finishedLessons = await context.StatusLessons.Where(sl => sl.StatusThemeId == info.StatusThemeId & sl.Status == "Пройден").ToListAsync();

            if (finishedLessons.Count() == info.AmountOfLessonsInTheme)
            {
                foreach (var statusLesson in finishedLessons)
                {
                    context.StatusLessons.Remove(statusLesson);
                }

                return finishedLessons[0].StatusThemeId;
            }

            return 0;
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
