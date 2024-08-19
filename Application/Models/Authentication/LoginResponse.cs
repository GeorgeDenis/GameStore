﻿namespace Application.Models.Authentication
{
    public class LoginResponse
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public string Token { get; set; }
        public DateTime ExpirationTime { get; set; }

    }
}
