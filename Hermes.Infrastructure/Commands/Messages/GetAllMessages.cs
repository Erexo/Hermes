using Hermes.Infrastructure.DTO;
using System;
using System.Collections.Generic;

namespace Hermes.Infrastructure.Commands.Messages
{
    public class GetAllMessages : IAuthenticatedCommand
    {
        public Guid ID { get; set; }

        public IEnumerable<MessageDTO> Messages { get; set; }
    }
}
