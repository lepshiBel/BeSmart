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
        private readonly IMapper mapper;

        public MembershipService(IRepositoryManager manager, IMapper mapper)
        {
            this.manager = manager;
            this.mapper = mapper;
        }

        public async Task<List<Membership>> GetAllMembershipsAsync()
        {
            var memberships = await manager.Membership.GetAllMembershipsWithUsersAsync();
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
            var course = await manager.Course.GetCourseWithThemesAsync(courseId);

            if (!course.CourseThemes.Any()) return created;

            foreach (var theme in course.CourseThemes)
            {
                await manager.StatusTheme.AddStatusTheme(theme.Id, membershipId);
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
    }
}
