using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleDotNETAPI.Models
{
    public class RedirectURI
    {
        [Key]
        public string UID { get; set; } = string.Empty;
        public string? URI { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public string? ApplicationUID { get; set; }
        public Application? Application { get; set; }

        public RedirectURI()
        {
            Guid uuid = Guid.NewGuid();
            this.UID = uuid.ToString();

            this.Created = DateTime.Now;
            this.Updated = DateTime.Now;
        }
    }
}