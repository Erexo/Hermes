using System;

namespace Hermes.Infrastructure.DTO
{
    public class UserDTO
    {
        public Guid ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool Verified { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
