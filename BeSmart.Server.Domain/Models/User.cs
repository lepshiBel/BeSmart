using BeSmart.Server.Domain.Models.Base;

namespace BeSmart.Server.Domain.Models
{
    public class User : EntityBase
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
