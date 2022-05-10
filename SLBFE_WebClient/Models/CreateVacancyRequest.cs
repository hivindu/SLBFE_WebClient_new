namespace SLBFE_WebClient.Models
{
    public class CreateVacancyRequest
    {
        public string JobTitle { get; set; } = string.Empty;
        public string JobDescription { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Deadline { get; set; } = string.Empty;
    }
}
