namespace BeSmart.Domain.DTOs.User
{
    public class UserLoginResponseDTO
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? Token { get; set; }

        public UserLoginResponseDTO(Models.User user, string token)
        {
            Id = user.Id;
            Username = user.Username;
            Email = user.Email;
            Role = user.Role;
            Token = token;
        }
    }
}
