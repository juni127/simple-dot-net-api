using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SimpleDotNETAPI.Models
{
    public class Client
    {
        [Key]
        public string UID { get; set; } = string.Empty;
        public string? PrivateKey { get; set; }
        public int MonthQueriesCount { get; set; }
        public ICollection<User> Users { get; } = new List<User>();
        public ICollection<Plan> Plans { get; } = new List<Plan>();
        public ICollection<Query> Queries { get; } = new List<Query>();
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }

        public Client()
        {
            Guid uuid = Guid.NewGuid();
            this.UID = uuid.ToString();

            this.Created = DateTime.Now;
            this.Updated = DateTime.Now;
        }

        public static void RelationsBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasMany(e => e.Plans)
                .WithOne(e => e.Client)
                .HasForeignKey(e => e.ClientUID)
                .HasPrincipalKey(e => e.UID);
            modelBuilder.Entity<Client>()
                .HasMany(e => e.Users)
                .WithOne(e => e.Client)
                .HasForeignKey(e => e.ClientUID)
                .HasPrincipalKey(e => e.UID);
            modelBuilder.Entity<Client>()
                .HasMany(e => e.Queries)
                .WithOne(e => e.Client)
                .HasForeignKey(e => e.ClientUID)
                .HasPrincipalKey(e => e.UID);
        }
    }
}