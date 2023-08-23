using System.ComponentModel.DataAnnotations;

namespace SimpleDotNETAPI.Models
{
    public class Plan
    {
        [Key]
        public string UID { get; set; } = string.Empty;
        public Model Model { get; set; } = Model.on_demand;
        public decimal UnitaryPrice { get; set; }
        public int QueriesLimit { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public string? ClientUID { get; set; }
        public Client? Client { get; set; }

        public Plan()
        {
            Guid uuid = Guid.NewGuid();
            this.UID = uuid.ToString();

            this.Created = DateTime.Now;
            this.Updated = DateTime.Now;
        }
    }

    public enum Model
    {
        on_demand,
        planned_usage
    }
}