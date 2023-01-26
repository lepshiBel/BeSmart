using BeSmart.Domain;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;

namespace BeSmart.Persistence.Repositories
{
    public class MembershipRepository : RepositoryBase<Membership, BeSmartDbContext>, IMembershipRepository
    {
        public MembershipRepository(BeSmartDbContext dbContext) : base(dbContext) { }
    }
}
