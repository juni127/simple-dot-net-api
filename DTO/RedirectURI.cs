namespace SimpleDotNETAPI.DTO
{
    public class RedirectURIDTO
    {
        public string UID { get; set; } = string.Empty;
        public string? URI { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public string? ApplicationUID { get; set; }
        public ApplicationDTO? Application { get; set; }
    }
}