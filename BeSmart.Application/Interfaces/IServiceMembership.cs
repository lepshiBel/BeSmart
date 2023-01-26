using BeSmart.Domain.Models;

namespace BeSmart.Application.Interfaces
{
    public interface IServiceMembership
    {
        Task<List<Membership>> GetAllMembershipsAsync();
        Task<Membership> FindMembershipByIdAsync(int id);
        Task<Membership> AddMembershipAsync(Membership answer);
        Task<Membership> DeleteMembershipAsync(int id);
    }
}
