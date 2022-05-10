namespace SLBFE_WebClient.Models
{
    public class Citizen
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Nic { get; set; } = string.Empty;
        public string Age { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
        public string Profession { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Affiliation { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Employed { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public bool Verified { get; set; } = false;
        public List<string>? Connections { get; set; }
    }
}
