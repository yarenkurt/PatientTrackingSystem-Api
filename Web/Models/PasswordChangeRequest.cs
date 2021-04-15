using System;

namespace Web.Models
{
    public class PasswordChangeRequest
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Token { get; set; }
        public bool IsUsed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UsedAt { get; set; }
        public DateTime ExpiredDate { get; set; }

    }
}