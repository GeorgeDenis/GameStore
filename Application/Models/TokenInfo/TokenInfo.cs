namespace Application.Models.TokenInfo
{
    public class TokenInfo
    {
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime ExpirationTime { get; set; }
    }
}
