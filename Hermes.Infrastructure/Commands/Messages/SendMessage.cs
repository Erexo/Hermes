using System;

namespace Hermes.Infrastructure.Commands.Messages
{
    public class SendMessage : IAuthenticatedCommand
    {
        public Guid ID { get; set; }
        public Guid InterlocutorId { get; set; }
        public string Content { get; set; }
    }
}
