namespace SLBFE_WebClient.Models
{
    public class CreateCompanyUserRequest
    {
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyAddress { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int UserType { get; set; }
    }
}
