using BeSmart.Domain;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace BeSmart.Persistence.Repositories
{
    public class ThemeStatusRepository : RepositoryBase<StatusTheme, BeSmartDbContext>, IStatusThemeRepository
    {
        public ThemeStatusRepository(BeSmartDbContext context) : base(context) { }
        public async Task<StatusTheme> AddStatusTheme(int themeId, int membershipId) 
        {
            StatusTheme newStatus = new StatusTheme() { ThemeId = themeId, MembershipId = membershipId};
            await context.StatusThemes.AddAsync(newStatus);
            await context.SaveChangesAsync();
            return newStatus;
        }

        public async Task<StatusThemeWithLessons> GetStatusThemeWithStatusLessonsWithLessonsAsync(int statusThemeId)
        {
            var statusThemeWithLessons = await context.StatusThemes.Include(x => x.StatusLessons).ThenInclude(x => x.Lesson)
                .Join(
                    context.Themes,
                    s => s.ThemeId,
                    t => t.Id,
                    (s, t) => new StatusThemeWithLessons
                    {
                        Id = s.Id,
                        CountOfLessons = t.CountLesson,
                        NameOfTheme = t.Name,
                        StatusLessons = new List<StatusLesson>(s.StatusLessons.Select(l =>
                        new StatusLesson
                        {
                            Id = l.Id,
                            Lesson = l.Lesson,
                            Status = l.Status
                        }))
                    })
                   .FirstOrDefaultAsync(x => x.Id == statusThemeId);

            return statusThemeWithLessons;
        }

        // проверяет равно ли кол-во пройденных уроков общему кол-ву уроков в теме, если равно,
        // то удаляет все записи из статус_лессон и статус_тест, относящиеся к пройденной теме
        public async Task<StatusTheme> CheckIfThemeIsPassed(int statusThemeId)
        {
            var statusThemeWithTheme = await context.StatusThemes.Include(s => s.Theme).FirstOrDefaultAsync(s => s.Id == statusThemeId);

            if (statusThemeWithTheme.AmountOfCompletedLessons == statusThemeWithTheme.Theme.CountLesson)
            {
                var finishedLessons = await context.StatusLessons.Where(sl => sl.StatusThemeId == statusThemeId & sl.Status == "Пройден").ToListAsync();
                var finishedTests = await context.StatusTests.Where(sl => sl.StatusThemeId == statusThemeId).ToListAsync();
                context.StatusLessons.RemoveRange(finishedLessons);
                context.StatusTests.RemoveRange(finishedTests); 
                await context.SaveChangesAsync(); // TODO ??

                return statusThemeWithTheme; 
            }

            return null;
        }

        // увеличивает кол-во пройденных уроков в статус_темы на 1
        public async Task<StatusTheme> UpdateAmountOfPassedLessons(int statusThemeId)
        {
            var statusThemeToUpdate = await context.StatusThemes.FindAsync(statusThemeId);
            if (statusThemeToUpdate == null) return null;
            statusThemeToUpdate.AmountOfCompletedLessons += 1;
            var updated = await base.UpdateAsync(statusThemeToUpdate.Id, statusThemeToUpdate);
            return updated;
        }

        public async Task<StatusTheme> UpdateStatusTheme(int statusThemeId, string newStatus)
        {
            var themeToUpdate = await context.StatusThemes.FindAsync(statusThemeId);
            if (themeToUpdate == null) return null;
            themeToUpdate.Status = newStatus;
            var updated = await base.UpdateAsync(themeToUpdate.Id, themeToUpdate);
            return updated;
        }
    }
}
