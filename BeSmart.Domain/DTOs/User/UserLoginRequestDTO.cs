using System.ComponentModel.DataAnnotations;

namespace BeSmart.Domain.DTOs.User
{
    public class UserLoginRequestDTO
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }

}
