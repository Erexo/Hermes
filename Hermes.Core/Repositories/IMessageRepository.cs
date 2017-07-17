using Hermes.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hermes.Core.Repositories
{
    public interface IMessageRepository : IRepository
    {
        Task<Message> GetAsync(Guid id);
        Task<IEnumerable<Message>> GetUserAsync(User user);
        Task<IEnumerable<Message>> GetConversationAsync(User user1, User user2);
        Task AddAsync(Message message);
        Task UpdateAsync(Message message);
        Task RemoveAsync(Message message);
        Task RemoveConversationAsync(User user1, User user2);
    }
}
