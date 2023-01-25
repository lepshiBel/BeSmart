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
        private readonly IServiceStatusTheme serviceStatusTheme;
        private readonly IMapper mapper;

        public MembershipService(IRepositoryManager manager, IMapper mapper,
            IServiceCourse serviceCourse, IServiceStatusTheme serviceStatusTheme)
        {
            this.manager = manager;
            this.mapper = mapper;
            this.serviceCourse = serviceCourse;
            this.serviceStatusTheme = serviceStatusTheme;
        }

        public async Task<List<Membership>> GetAllMembershipsAsync()
        {
            var memberships = await manager.Membership.GetAllMembershipsWithUserAsync();
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

            foreach (var theme in course.Themes)
            {
                await serviceStatusTheme.AddStatusThemeAsync(theme.Id, membershipId);
            }

            return created;
        }

        public async Task<Membership> DeleteMembershipAsync(int id)
        {
            return await manager.Membership.DeleteAsync(id);
        }

        public async Task<MembershipWithThemesDTO> GetMembershipWithThemesForUserAsync(int membershipId)
        {
            var membership =  await manager.Membership.GetMembershipWithThemesAsync(membershipId);
            return mapper.Map<MembershipWithThemesDTO>(membership);
        }

        // проверяет пройдены ли все темы в курсе 
        public async Task<bool?> CheckIfAllThemesArePassedAsync(int membershipId)
        {
            var membershipToCheck = await manager.Membership.GetMembershipWithCourseAsync(membershipId);

            if (membershipToCheck == null) return null;

            return membershipToCheck.AmountOfCompletedThemes == membershipToCheck.Course.CountOfThemes ? true : false;
        }
        
        // обновляет статус membership на завершен и удаляет все темы, связанные с курсом
        public async Task<MembershipDTO> FinishCourseLearning(int membershipId)
        {
            var membership = await  manager.Membership.GetMembershipWithCourseAsync(membershipId);
            membership.Status = "Завершен";
            var updated = await manager.Membership.UpdateAsync(membershipId, membership); // todo dto
            await manager.StatusTheme.FindStatusThemesAndDeleteArrange(membershipId);
            return mapper.Map<MembershipDTO>(updated);
        }

    }
}
