using BeSmart.Domain.Models;

namespace BeSmart.Domain.Interfaces
{
    public interface ILessonRepository : IRepositoryBase<Lesson>
    {
        Task<Lesson> GetLessonWithCardsAsync(int id);
    }
}
