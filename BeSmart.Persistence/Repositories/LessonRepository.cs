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
    }
}
