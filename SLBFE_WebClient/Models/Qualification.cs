namespace SLBFE_WebClient.Models
{
    public class Qualification
    {
        public string Id { get; set; } = string.Empty;
        public string QualificationName { get; set; } = string.Empty;
        public byte[] CertificateImage { get; set; } = null!;
        public string CitizenId { get; set; } = string.Empty;
    }
}
