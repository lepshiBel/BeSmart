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

        //public async Task<StatusTheme> GetStatusThemeWithLessonsAndTestsAsync(int statusThemeId)
        //{
        //    //var statusTheme = await context.StatusThemes.FindAsync(statusThemeId);
        //    //await context.Entry(statusTheme).Collection(x => x.StatusLessons).LoadAsync();
        //    //return statusTheme;
        //    var ldetails = context.StatusThemes.Include(i => i.ListFriends).SingleOrDefault(c => c.UserName == registrationUser.UserName && c.Password == registrationUser.Password);
        //}

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

            //var statusThemeWithStatusLessonsWithLessons = context.StatusThemes.Include(x => x.Theme)
            //    .Include(x => x.StatusLessons)
            //    .ThenInclude(x => x.Lesson)
            //    .FirstOrDefault(x => x.Id == statusThemeId);
            //return statusThemeWithStatusLessonsWithLessons;
        }

        public async Task<StatusTheme> UpdateStatusTheme(int statusThemeId, string newStatus)
        {
            var themeToUpdate = await context.StatusThemes.FindAsync(statusThemeId);
            if (themeToUpdate == null) return null;
            themeToUpdate.Status = newStatus;
            context.StatusThemes.Update(themeToUpdate);
            await context.SaveChangesAsync();
            return themeToUpdate;
        }
    }
}
