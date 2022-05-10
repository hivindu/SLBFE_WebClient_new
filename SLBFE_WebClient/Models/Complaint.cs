namespace SLBFE_WebClient.Models
{
    public class Complaint
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string ComplaintMessage { get; set; } = string.Empty;
        public string? RespondedUserId { get; set; }
        public string? ResponseMessage { get; set; }
    }
}
