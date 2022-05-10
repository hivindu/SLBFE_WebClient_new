﻿namespace SLBFE_WebClient.Models
{
    public class CreateUserRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int UserType { get; set; } = 0;
    }
}
