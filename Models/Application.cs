using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SimpleDotNETAPI.Models
{
    public class Application
    {
        [Key]
        public string UID { get; set; } = string.Empty;
        public string SecretKey { get; set; }
        public string PublicKey { get; set; }
        public ICollection<RedirectURI> RedirectURIs { get; } = new List<RedirectURI>();
        public ICollection<Token> Tokens { get; } = new List<Token>();
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public Application()
        {
            this.UID = Guid.NewGuid().ToString();

            this.SecretKey = Guid.NewGuid().ToString();
            this.PublicKey = Guid.NewGuid().ToString();

            this.Created = DateTime.Now;
            this.Updated = DateTime.Now;
        }

        public static void RelationsBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>()
                .HasMany(e => e.RedirectURIs)
                .WithOne(e => e.Application)
                .HasForeignKey(e => e.ApplicationUID)
                .HasPrincipalKey(e => e.UID);
            modelBuilder.Entity<Application>()
                .HasMany(e => e.Tokens)
                .WithOne(e => e.Application)
                .HasForeignKey(e => e.ApplicationUID)
                .HasPrincipalKey(e => e.UID);
        }
    }
}