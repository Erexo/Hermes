using Hermes.Core.Domain;
using Hermes.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hermes.Infrastructure.Services
{
    public interface IMessageService : IService
    {
        Task<Message> GetAsync(Guid id);
        Task<IEnumerable<MessageDTO>> GetUserAsync(Guid userId);
        Task<IEnumerable<Message>> GetConversationAsync(Guid user1Id, Guid user2Id);
        Task SendAsync(Guid senderId, Guid receiverId, string content);
        Task SetAsDisplayedAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task RemoveConversationAsync(Guid user1Id, Guid user2Id);

    }
}
