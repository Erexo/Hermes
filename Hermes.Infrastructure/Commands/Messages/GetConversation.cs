using Hermes.Core.Domain;
using System;
using System.Collections.Generic;

namespace Hermes.Infrastructure.Commands.Messages
{
    public class GetConversation : IAuthenticatedCommand
    {
        public Guid ID { get; set; }
        public Guid InterlocutorID { get; set; }

        public IEnumerable<Message> Messages { get; set; }
    }
}
