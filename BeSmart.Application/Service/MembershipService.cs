using AutoMapper;
using BeSmart.Application.Interfaces;
using BeSmart.Domain.DTOs.Membership;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Service
{
    public class MembershipService : IServiceMembership
    {
        private readonly IRepositoryManager manager;
        private readonly IServiceCourse serviceCourse;
        private readonly IServiceTheme serviceTheme;
        private readonly IServiceStatusTheme serviceStatusTheme;
        private readonly IServiceStatusLesson serviceStatusLesson;
        private readonly IMapper mapper;

        public MembershipService(IRepositoryManager manager, IMapper mapper,
            IServiceCourse serviceCourse, IServiceStatusTheme serviceStatusTheme, 
            IServiceTheme serviceTheme, IServiceStatusLesson serviceStatusLesson)
        {
            this.manager = manager;
            this.mapper = mapper;
            this.serviceCourse = serviceCourse;
            this.serviceStatusTheme = serviceStatusTheme;
            this.serviceTheme = serviceTheme;
            this.serviceStatusLesson = serviceStatusLesson;
        }

        public async Task<List<Membership>> GetAllMembershipsAsync()
        {
            var memberships = await manager.Membership.GetAllAsync();
            return memberships;
        }

        public async Task<List<MembershipDTO>> GetAllMembershipsForUserAsync(int userId)
        {
            var memberships = await manager.Membership.GetMembershipsForUserAsync(userId);

            if(!memberships.Any()) return null;

            return mapper.Map<List<MembershipDTO>>(memberships);
        }

        public bool CheckMembershipAsync(int courseId, int userId)
        {
            var existed = manager.Membership.CheckIfExisted(courseId, userId);
            return existed;
        }
        public async Task<Membership> CreateNewMembershipAsync(int courseId, int userId)
        {
            var newMembership = new Membership() { CourseId = courseId, UserId = userId, Status = "В процессе" };
            var created = await manager.Membership.AddAsync(newMembership);
            var membershipId = created.Id;
            var course = await serviceCourse.GetCourseWithThemesAsync(courseId);
            if(course == null) return null;

            List<StatusTheme> createdStatusThemes = new List<StatusTheme>();
            List<StatusLesson> createdStatusLessons = new List<StatusLesson>();
            //List<StatusTest> createdStatusTests = new List<StatusTest>();

            foreach (var theme in course.Themes)
            {
                createdStatusThemes.Add(await serviceStatusTheme.AddStatusThemeAsync(theme.Id, membershipId));
            }

            foreach (var statusTheme in createdStatusThemes)
            {
                var themeWithLessons = await serviceTheme.GetThemeWithLessonsAsync(statusTheme.ThemeId);

                var themeWithTests = await serviceTheme.GetThemeWithTestsAsync(statusTheme.ThemeId);

                foreach (var lesson in themeWithLessons.Lessons)
                {
                    createdStatusLessons.Add(await serviceStatusLesson.AddStatusLessonAsync(lesson.Id, statusTheme.Id));
                }
            }

            return created;
        }

        public async Task<Membership> DeleteMembershipAsync(int id)
        {
            return await manager.Membership.DeleteAsync(id);
        }
    }
}
