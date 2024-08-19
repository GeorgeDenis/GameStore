using Domain.Common;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;

namespace Application.Models.TokenInfo
{
    public class TokenInfo
    {
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserRole { get; set; }
        public DateTime ExpirationTime { get; set; }
        public string Fingerprint { get; set; }
        public static string GetFingerprint(HttpRequest request)
        {
            var userAgent = request.Headers["User-Agent"].ToString();
            var ipAddress = request.HttpContext.Connection.RemoteIpAddress.ToString();
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(userAgent + ipAddress));
                return Convert.ToBase64String(hash);
            }
        }
    }

}
