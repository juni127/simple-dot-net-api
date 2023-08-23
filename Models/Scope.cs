using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SimpleDotNETAPI.Models
{
    public class Scope
    {
        [Key]
        public string UID { get; set; } = string.Empty;
        public string? Action { get; set; }
        public ICollection<Token> Tokens { get; } = new List<Token>();
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public Scope()
        {
            Guid uuid = Guid.NewGuid();
            this.UID = uuid.ToString();

            this.Created = DateTime.Now;
            this.Updated = DateTime.Now;
        }

        public Scope(string action)
        {
            Guid uuid = Guid.NewGuid();
            this.UID = uuid.ToString();

            this.Action = action;

            this.Created = DateTime.Now;
            this.Updated = DateTime.Now;
        }

        public static void RelationsBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Scope>()
                .HasMany(e => e.Tokens)
                .WithMany(e => e.Scopes);
        }
    }
}