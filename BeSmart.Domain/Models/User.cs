using BeSmart.Domain.Models.Base;

namespace BeSmart.Domain.Models
{
    public class User : EntityBase
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? Role { get; set; }
        public List<Course> CreatedCourses { get; set; }
        public List<Membership> Memberships { get; set; }
    }
}
