using Microsoft.EntityFrameworkCore;
using SimpleDotNETAPI.Models;

namespace SimpleDotNETAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Scope> Scopes { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<RedirectURI> RedirectURIs { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Query> Queries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Client.RelationsBuilder(modelBuilder);
            User.RelationsBuilder(modelBuilder);
            Token.RelationsBuilder(modelBuilder);
            Scope.RelationsBuilder(modelBuilder);
            Application.RelationsBuilder(modelBuilder);
        }
    }

}