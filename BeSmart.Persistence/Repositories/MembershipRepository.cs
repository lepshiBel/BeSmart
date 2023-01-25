using BeSmart.Domain;
using BeSmart.Domain.DTOs.Membership;
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

        public async Task<List<Membership>> GetAllMembershipsWithUserAsync()
        {
            var membershipsWithUsers = await context.Memberships.Include(x => x.User).ToListAsync();
            return membershipsWithUsers;
        }

        public async Task<List<Membership>> GetMembershipsForUserAsync(int userId)
        {
            var memberships = await context.Memberships.Where(m => m.UserId==userId)
                .Include(x => x.Course)
                .Include(m=>m.Course.Category)
                .ToListAsync();

            return memberships;
        }

        public async Task<Membership> GetMembershipWithCourseAsync(int membershipId)
        {
            return await context.Memberships.Include(m=>m.Course).ThenInclude(m => m.Category).FirstOrDefaultAsync(m=>m.Id==membershipId);
        }

        public async Task<Membership> GetMembershipWithThemesAsync(int membershipId) 
            // TODO load only necessary fields
        {
            var membership = await context.Memberships.Include(m=>m.Course).FirstOrDefaultAsync(x => x.Id == membershipId);

            if (membership == null) return null;

            await context.Entry(membership).Collection(x => x.StatusThemes).LoadAsync();

            foreach (var statueTheme in membership.StatusThemes)
            {
                await context.Entry(statueTheme).Reference(x => x.Theme).LoadAsync();
            }

            return membership;
        }

        // увеличивает кол-во пройденных тем в membership
        public async Task<Membership> UpdateAmountOfCompletedThemesAsync(int membershipId)
        {
            var membership = await context.Memberships.Include(x => x.Course).FirstOrDefaultAsync(m=>m.Id == membershipId);

            if(membership == null) return null;

            membership.AmountOfCompletedThemes += 1;

            var updated = await base.UpdateAsync(membership.Id, membership); // TODO изменить реализацию метода
            return updated;
        }
    }
}
