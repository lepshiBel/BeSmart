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
        public async Task<Membership> FindMembershipByIdAsync(int id)
        {
            var membership = await manager.Membership.GetAsync(id);
            return membership;
        }

        public async Task<List<Membership>> GetAllMembershipsAsync()
        {
            var memberships = await manager.Membership.GetAllAsync();
            return memberships;
        }

        public Task<Membership> AddMembershipAsync(Membership answer)
        {
            throw new NotImplementedException();
        }

        public async Task<Membership> DeleteMembershipAsync(int id)
        {
            return await manager.Membership.DeleteAsync(id);
        }
    }
}
