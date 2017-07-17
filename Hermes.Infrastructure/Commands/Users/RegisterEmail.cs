using System;

namespace Hermes.Infrastructure.Commands.Users
{
    public class RegisterEmail : IAuthenticatedCommand
    {
        public Guid ID { get; set; }
        public string Code { get; set; }
    }
}
