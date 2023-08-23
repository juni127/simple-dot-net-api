using SimpleDotNETAPI.Models;

namespace SimpleDotNETAPI.DTO
{
    public class UserDTO
    {
        public string UID { get; set; } = string.Empty;
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public Client Client { get; set; }
        public Role? Role { get; set; }
    }
}