using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SimpleDotNETAPI.Models
{
    public class User
    {
        [Key]
        public string UID { get; set; } = string.Empty;
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string? ClientUID { get; set; }
        public Client? Client { get; set; }
        public Role? Role { get; set; }
        public ICollection<Token> Tokens { get; } = new List<Token>();
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }

        public User(string firstname, string lastname, string username, string email, string password, string salt)
        {
            Guid uuid = Guid.NewGuid();
            this.UID = uuid.ToString();

            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Username = username;
            this.Email = email;
            this.Password = password;
            this.Salt = salt;

            this.Created = DateTime.Now;
            this.Updated = DateTime.Now;
        }

        public static void RelationsBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.Tokens)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserUID)
                .HasPrincipalKey(e => e.UID);
        }
    }

    public enum Role
    {
        provider,
        client
    }
}