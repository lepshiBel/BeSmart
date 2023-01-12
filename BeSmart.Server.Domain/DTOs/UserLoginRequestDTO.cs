using System.ComponentModel.DataAnnotations;

namespace BeSmart.Server.Domain.DTOs
{
    public class UserLoginRequestDTO
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
