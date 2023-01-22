using BeSmart.Domain;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;

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

        public async Task<StatusTheme> UpdateStatusTheme(StatusTheme statusTheme, string newStatus)
        {
            statusTheme.Status = newStatus;
            context.StatusThemes.Update(statusTheme);
            await context.SaveChangesAsync();
            return statusTheme;
        }
    }
}
