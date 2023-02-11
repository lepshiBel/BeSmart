using BeSmart.Domain;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace BeSmart.Persistence.Repositories
{
    public class TestRepository : RepositoryBase<Test, BeSmartDbContext>, ITestRepository
    {
        public TestRepository(BeSmartDbContext dbContext) : base(dbContext) { }

        public async Task<Test> GetTestWithQuestionsAsync(int id)
        {
            var test = await context.Tests.Include(t => t.Questsions).ThenInclude(q => q.Answers).FirstOrDefaultAsync(t => t.Id == id);
            return test;
        }
        public override async Task<Test> UpdateAsync(int id, Test test)
        {
            var old = context.Tests.FirstOrDefault(l => l.Id == id);

            if (old == null)
            {
                return null;
            }

            old.Name = test.Name;
            old.QuestionsCount = test.QuestionsCount;
            var updated = await base.UpdateAsync(id, old);
            return updated;
        }
    }
}
