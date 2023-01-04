using BeSmart.Domain;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;

namespace BeSmart.Persistence.Repositories
{
    public class TestRepository : RepositoryBase<Test, BeSmartDbContext>, ITestRepository
    {
        public TestRepository(BeSmartDbContext dbContext) : base(dbContext) { }

        public async Task<Test> GetTestWithQuestionsAsync(int id)
        {
            var test = await context.Tests.FindAsync(id);
            await context.Entry(test).Collection(q => q.Questsions).LoadAsync();
            return test;
        }
    }
}
