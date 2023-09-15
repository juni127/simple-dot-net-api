using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SimpleDotNETAPI.Models
{
    public class Processo
    {
        [Key]
        public string ID { get; set; } = string.Empty;

        public ICollection<Comite> Comites { get; set; } = new List<Comite>()

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public Processo()
        {
            this.UID = Guid.NewGuid().ToString();


            this.Created = DateTime.Now;
            this.Updated = DateTime.Now;
        }

        public static void RelationsBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Processo>()
                .HasMany(e => e.Comites)
                .WithOne(e => e.Processo)
                .HasForeignKey(e => e.ProcessoID)
                .HasPrincipalKey(e => e.ID);
        }
    }
}