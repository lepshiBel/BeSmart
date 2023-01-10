using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Domain;
using BeSmart.Persistence.Data;

namespace BeSmart.Persistence.Repositories
{
    public class LessonRepository : RepositoryBase<Lesson, BeSmartDbContext>, ILessonRepository
    {
        public LessonRepository(BeSmartDbContext dbContext) : base(dbContext) { }

        public async Task<Lesson> GetLessonWithCardsAsync(int id)
        {
            var lesson = await context.Lessons.FindAsync(id);
            await context.Entry(lesson).Collection(q => q.Cards).LoadAsync();
            return lesson;
        }

        public override async Task<Lesson> UpdateAsync(int id, Lesson lesson)
        {
            var old = context.Lessons.FirstOrDefault(l => l.Id == id);

            if (old == null)
            {
                return null;
            }

            old.Text = lesson.Text;
            old.Name = lesson.Name;
            var updated = await base.UpdateAsync(id, old);
            return updated;
        }
    }
}
