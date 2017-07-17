using System;

namespace Hermes.Infrastructure.Commands.Messages
{
    public class DeleteMessages : IAuthenticatedCommand
    {
        public Guid ID { get; set; }
        public Guid InterlocutorID { get; set; }
    }
}
