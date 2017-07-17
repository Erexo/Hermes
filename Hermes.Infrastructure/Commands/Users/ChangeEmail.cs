using System;

namespace Hermes.Infrastructure.Commands.Users
{
    public class ChangeEmail : IAuthenticatedCommand
    {
        public Guid ID { get; set; }
        public string OldEmail { get; set; }
        public string NewEmail { get; set; }
    }
}
