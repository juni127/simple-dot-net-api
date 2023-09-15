using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SimpleDotNETAPI.Models
{
    public class Comite
    {
        [Key]
        public string ID { get; set; } = string.Empty;

        public string ProcessoID { get; set; }
        public Processo processo { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public Comite()
        {
            this.UID = Guid.NewGuid().ToString();


            this.Created = DateTime.Now;
            this.Updated = DateTime.Now;
        }

        public static void RelationsBuilder(ModelBuilder modelBuilder)
        {
        }
    }
}