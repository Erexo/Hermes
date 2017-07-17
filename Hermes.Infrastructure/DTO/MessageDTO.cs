using Hermes.Core.Domain;
using System;

namespace Hermes.Infrastructure.DTO
{
    public class MessageDTO
    {
        public Guid ID { get; protected set; }
        public Guid ToUserID { get; protected set; }
        public string Content { get; protected set; }
        public bool Displayed { get; protected set; }
        public DateTime Date { get; protected set; }
    }
}
