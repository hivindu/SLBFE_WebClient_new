namespace SLBFE_WebClient.Models
{
    public class ReplyToComplainRequest
    {
        public string ComplainId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Reply { get; set; } = string.Empty;
    }
}
