using Hermes.Infrastructure.DTO;
using System;

namespace Hermes.Infrastructure.Commands.Users
{
    public class GetUser : IAuthenticatedCommand
    {
        public Guid ID { get; set; }
        public UserDTO User { get; set; }
    }
}
