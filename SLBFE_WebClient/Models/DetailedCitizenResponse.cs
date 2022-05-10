namespace SLBFE_WebClient.Models
{
    public class DetailedCitizenResponse
    {
        public Citizen CitizenBasicDetails { get; set; } = new();
        public List<Qualification> QuolificationsList { get; set; } = new();
    }
}
