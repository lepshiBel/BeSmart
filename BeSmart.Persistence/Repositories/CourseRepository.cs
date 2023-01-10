using BeSmart.Domain;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;

namespace BeSmart.Persistence.Repositories
{
    public class CourseRepository : RepositoryBase<Course, BeSmartDbContext>, ICourseRepository
    {
        public CourseRepository(BeSmartDbContext context) : base(context) { }

        public async Task<Course> GetCourseWithThemesAsync(int id)
        {
            var course = await context.Courses.FindAsync(id);
            await context.Entry(course).Collection(q => q.Themes).LoadAsync();
            return course;
        }
        public override async Task<Course> UpdateAsync(int id, Course course)
        {
            var old = context.Courses.FirstOrDefault(o => o.Id == id);

            if (old == null)
            {
                return null;
            }

            old.Name = course.Name;
            old.CategoryId = course.CategoryId;
            var updated = await base.UpdateAsync(id, old);
            return updated;
        }
    }
}
