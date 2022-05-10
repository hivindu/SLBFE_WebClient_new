namespace SLBFE_WebClient.Models
{
    public class JobVacencies
    {
        public string Id { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string JobDescription { get; set; } = string.Empty;
        public bool IsApproved { get; set; } = false;
        public string UserId { get; set; } = string.Empty;
        public List<string>? Candidates { get; set; }
        public string Deadline { get; set; } = string.Empty;
    }
}
