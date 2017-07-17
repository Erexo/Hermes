using System;

namespace Hermes.Infrastructure.Commands.Users
{
    public class ChangePassword : IAuthenticatedCommand
    {
        public Guid ID { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
