using BeSmart.Domain.Models;

namespace BeSmart.Domain.Interfaces
{
    public interface ICourseRepository : IRepositoryBase<Course>
    {
        Task<Course> GetCourseWithThemesAsync(int id);
    }
}
