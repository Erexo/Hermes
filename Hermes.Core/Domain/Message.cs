using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Hermes.Core.Domain
{
    public class Message
    {
        [BsonId]
        public Guid ID { get; protected set; }
        public Guid FromUserID { get; protected set; }
        public Guid ToUserID { get; protected set; }
        public string Content { get; protected set; }
        public bool Displayed { get; protected set; }
        public DateTime Date { get; protected set; }

        public Message(User fromUser, User toUser, string content)
        {
            ID = Guid.NewGuid();
            FromUserID = fromUser.ID;
            ToUserID = toUser.ID;
            Content = content;
            Displayed = false;
            Date = DateTime.Now;
        }

        protected Message()
        {
        }

        public void SetAsDisplayed()
            => Displayed = true;
    }
}
