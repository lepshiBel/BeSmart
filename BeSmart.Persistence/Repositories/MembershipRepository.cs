using BeSmart.Domain;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;

namespace BeSmart.Persistence.Repositories
{
    public class MembershipRepository : RepositoryBase<Membership, BeSmartDbContext>, IMembershipRepository
    {
        public MembershipRepository(BeSmartDbContext context) : base(context) { }

        public async Task<List<Membership>> GetMembershipsForUser(int userId)
        {
            var memberships = context.Memberships.Where(m => m.UserId==userId).ToList();
            return memberships;
        }
    }
}
