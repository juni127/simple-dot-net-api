using System.ComponentModel.DataAnnotations;

namespace SimpleDotNETAPI.Models
{
    public class Query
    {
        [Key]
        public string UID { get; set; } = string.Empty;
        public string MetaData { get; set; } = string.Empty;
        public Status Status { get; set; } = Status.created;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public string? ClientUID { get; set; }
        public Client? Client { get; set; }

        public Query()
        {
            Guid uuid = Guid.NewGuid();
            this.UID = uuid.ToString();

            this.Created = DateTime.Now;
            this.Updated = DateTime.Now;
        }
    }

    public enum Status
    {
        created,
        succeded,
        failed,
        denied,
    }
}