using SimpleDotNETAPI.Models;

namespace SimpleDotNETAPI.DTO
{
    public class ApplicationDTO
    {
        public string UID { get; set; } = string.Empty;
        public string? PublicKey { get; set; }
        public ICollection<RedirectURI>? RedirectURIs { get; set; }
        public ICollection<User>? Users { get; set; }
    }
}