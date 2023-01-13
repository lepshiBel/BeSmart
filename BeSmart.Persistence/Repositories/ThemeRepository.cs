using BeSmart.Domain;
using BeSmart.Domain.DTOs.Theme;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;

namespace BeSmart.Persistence.Repositories
{
    public class ThemeRepository : RepositoryBase<Theme, BeSmartDbContext>, IThemeRepository
    {
        public ThemeRepository(BeSmartDbContext context) : base(context) { }

        public async Task<Theme> GetThemeWithLessonsAsync(int id)
        {
            var theme = await context.Themes.FindAsync(id);
            await context.Entry(theme).Collection(t => t.Lessons).LoadAsync();
            return theme;
        }

        public async Task<Theme> GetThemeWithTestsAsync(int id)
        {
            var theme = await context.Themes.FindAsync(id);
            await context.Entry(theme).Collection(t => t.Tests).LoadAsync();
            return theme;       
        }

        public override async Task<Theme> UpdateAsync(int id, Theme theme)
        {
            var old = context.Themes.FirstOrDefault(o => o.Id == id);

            if (old == null)
            {
                return null;
            }

            old.Name = theme.Name;
            old.CountLesson = theme.CountLesson;
            old.CountTest = theme.CountTest;
            old.CourseId = theme.CourseId;
            var updated = await base.UpdateAsync(id, old);
            return updated;
        }
    }
}
