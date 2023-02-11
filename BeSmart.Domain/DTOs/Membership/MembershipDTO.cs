namespace BeSmart.Domain.DTOs.Membership
{
    public class MembershipDTO
    {
        public int? Id { get; set; }
        public string? NameOfCourse { get; set; }
        public string? NameOfCourseCategory { get; set; }
        public string? Status { get; set; }
        public int? CountOfThemes { get; set; }
    }
}
