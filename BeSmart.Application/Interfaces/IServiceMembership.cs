using BeSmart.Domain.DTOs.Membership;
using BeSmart.Domain.Models;

namespace BeSmart.Application.Interfaces
{
    public interface IServiceMembership
    {
        Task<List<Membership>> GetAllMembershipsAsync();
        bool CheckMembershipAsync(int courseId, int userId);
        Task<List<MembershipDTO>> GetAllMembershipsForUserAsync(int userId);
        Task<MembershipWithThemesDTO> GetMembershipWithThemesForUserAsync(int membershipId);
        Task<Membership> CreateNewMembershipAsync(int courseId, int userId);
        Task<Membership> DeleteMembershipAsync(int id);
        Task<bool?> CheckIfAllThemesArePassedAsync(int membershipId);
        Task<MembershipDTO> FinishCourseLearning(int membershipId);
    }
}
