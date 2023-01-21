using BeSmart.Domain;
using BeSmart.Domain.Interfaces;
using BeSmart.Domain.Models;
using BeSmart.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace BeSmart.Persistence.Repositories
{
    public class MembershipRepository : RepositoryBase<Membership, BeSmartDbContext>, IMembershipRepository
    {
        public MembershipRepository(BeSmartDbContext context) : base(context) { }

        public bool CheckIfExisted(int courseId, int userId)
        {
            var membership = context.Memberships.Where(m=>m.UserId == userId && m.CourseId == courseId).FirstOrDefault();
            return membership == null? false: true;
        }

        public override async Task<List<Membership>> GetAllAsync()
        {
            return await context.Memberships.Include(x=>x.User).ToListAsync();
        }

        public async Task<List<Membership>> GetMembershipsForUserAsync(int userId)
        {
            var memberships = await context.Memberships.Where(m => m.UserId==userId)
                .Include(x => x.Course)
                .Include(m=>m.Course.Category)
                .ToListAsync();

            return memberships;
        }
    }
}
