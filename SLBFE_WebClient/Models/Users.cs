namespace SLBFE_WebClient.Models
{
    public class Users
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Company? CompanyDetails { get; set; }
        public bool IsActive { get; set; } = false;
        public int UserType { get; set; } = 0;
    }
}
