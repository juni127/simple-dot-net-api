using SimpleDotNETAPI.Models;

namespace SimpleDotNETAPI.DTO
{
    public class ClientDTO
    {
        public string UID { get; set; } = string.Empty;
        public string? PrivateKey { get; set; }
        public int MonthQueriesCount { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public ICollection<User>? Users { get; set; }
    }
}