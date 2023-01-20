using BeSmart.Application.Interfaces;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Service
{
    public class MembershipService : IServiceMembership
    {
        private readonly IRepositoryManager manager;
        public MembershipService(IRepositoryManager manager)
        {
            this.manager = manager;
        }
        public async Task<List<Membership>> GetAllMembershipsAsync()
        {
            var memberships = await manager.Membership.GetAllAsync();
            return memberships;
        }
        public async Task<List<Membership>> GetAllMembershipsForUserAsync(int userId)
        {
            var memberships = await manager.Membership.GetAllAsync();
            return memberships;
        }

        public async Task<Membership> FindMembershipByIdAsync(int id)
        {
            var membership = await manager.Membership.GetAsync(id);
            return membership;
        }


        public async Task<Membership> CreateNewMembershipAsync(int courseId, int userId)
        {
            var newMembership = new Membership() { CourseId = courseId, UserId = userId };
            var created = await manager.Membership.AddAsync(newMembership);
            return created;
        }

        public async Task<Membership> DeleteMembershipAsync(int id)
        {
            return await manager.Membership.DeleteAsync(id);
        }
    }
}
