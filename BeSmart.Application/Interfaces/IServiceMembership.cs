using BeSmart.Domain.Models;

namespace BeSmart.Application.Interfaces
{
    public interface IServiceMembership
    {
        Task<List<Membership>> GetAllMembershipsAsync();
        Task<List<Membership>> GetAllMembershipsForUserAsync(int userId);
        Task<Membership> CreateNewMembershipAsync(int courseId, int userId);
        Task<Membership> DeleteMembershipAsync(int id);
    }
}
