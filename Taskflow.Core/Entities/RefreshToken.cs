using System;

namespace TaskFlow.Core.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string CreatedByIp { get; set; } = string.Empty;
        public DateTime? Revoked { get; set; }
        public string? ReplacedByToken { get; set; }

        public string ApplicationUserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }
    }
}
