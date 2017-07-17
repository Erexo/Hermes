using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hermes.Core.Domain;
using Hermes.Core.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;

namespace Hermes.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMongoDatabase _database;
        private IMongoCollection<Message> Messages => _database.GetCollection<Message>("Messages");

        public MessageRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Message> GetAsync(Guid id)
            => await Messages.AsQueryable().FirstOrDefaultAsync(o => o.ID == id);

        public async Task<IEnumerable<Message>> GetConversationAsync(User user1, User user2)
            => await Messages.AsQueryable().
                Where(o => o.FromUserID == user1.ID && o.ToUserID == user2.ID || o.FromUserID == user2.ID && o.ToUserID == user1.ID).ToListAsync();

        public async Task<IEnumerable<Message>> GetUserAsync(User user)
            => await Messages.AsQueryable().Where(o => o.ToUserID == user.ID).ToListAsync();

        public async Task AddAsync(Message message)
            => await Messages.InsertOneAsync(message);

        public async Task RemoveAsync(Message message)
            => await Messages.DeleteOneAsync(o => o.ID == message.ID);

        public async Task RemoveConversationAsync(User user1, User user2)
        {
            var messages = await this.GetConversationAsync(user1, user2);
            await Messages.DeleteManyAsync(o => messages.Any(x => o.ID == x.ID));
        }

        public async Task UpdateAsync(Message message)
            => await Messages.ReplaceOneAsync(o => o.ID == message.ID, message);
    }
}
