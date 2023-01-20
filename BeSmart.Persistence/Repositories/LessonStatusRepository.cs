using BeSmart.Domain;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;

namespace BeSmart.Persistence.Repositories
{
    public class LessonStatusRepository : RepositoryBase<StatusLesson, BeSmartDbContext>, IStatusLessonRepository
    {
        public LessonStatusRepository(BeSmartDbContext context) : base(context) { }
    }
}
