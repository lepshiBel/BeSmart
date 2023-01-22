using BeSmart.Domain.Models;

namespace BeSmart.Domain.Interfaces
{
    public interface IMembershipRepository : IRepositoryBase<Membership>
    {
        Task<List<Membership>> GetMembershipsForUserAsync(int userId);
        Task<Membership> GetMembershipWithThemesAsync(int membershipId);
        bool CheckIfExisted(int courseId, int userId);
    }
}
