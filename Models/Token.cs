using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SimpleDotNETAPI.Models
{
    public class Token
    {
        [Key]
        public string UID { get; set; } = string.Empty;
        public TokenType Type { get; set; } = TokenType.Access;
        public string? FingerPrint { get; set; }
        public ICollection<Scope> Scopes { get; } = new List<Scope>();
        public DateTime TimeToLive { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public string? ApplicationUID { get; set; }
        public Application? Application { get; set; }

        public string? RefreshTokenUID { get; set; }
        public virtual Token? RefreshToken { get; set; }

        public string? AccessTokenUID { get; set; }
        public virtual Token? AccessToken { get; set; }

        public string? UserUID { get; set; }
        public User? User { get; set; }

        public Token()
        {
            Guid uuid = Guid.NewGuid();
            this.UID = uuid.ToString();

            this.Created = DateTime.Now;
            this.Updated = DateTime.Now;
        }

        public static void RelationsBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Token>()
                .HasMany(e => e.Scopes)
                .WithMany(e => e.Tokens);
            modelBuilder.Entity<Token>()
                .HasOne(e => e.RefreshToken)
                .WithOne(e => e.AccessToken)
                .HasForeignKey<Token>(e => e.AccessTokenUID)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Token>()
                .HasOne(e => e.AccessToken)
                .WithOne(e => e.RefreshToken)
                .HasForeignKey<Token>(e => e.RefreshTokenUID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class TokenObject
    {
        public string UID { get; set; } = string.Empty;
        public TokenType Type { get; set; } = TokenType.Access;
        public DateTime TimeToLive { get; set; }
        public virtual TokenObject? Refresh { get; set; }
    }

    public enum TokenType
    {
        Access,
        Refresh,
        Authorization,
    }
}