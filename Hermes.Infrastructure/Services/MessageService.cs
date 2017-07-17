using AutoMapper;
using Hermes.Core.Domain;
using Hermes.Core.Repositories;
using Hermes.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hermes.Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public MessageService(IUserRepository userRepository, IMessageRepository messageRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<Message> GetAsync(Guid id)
        {
            var message = await _messageRepository.GetAsync(id);

            if (message == null)
                throw new Exception($"Message with ID {id} does not exist.");

            return message;
        }

        public async Task<IEnumerable<MessageDTO>> GetUserAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);

            if (user == null)
                throw new Exception($"User with id {userId} does not exist.");

            var messages = await _messageRepository.GetUserAsync(user);

            var messagesDto = _mapper.Map<IEnumerable<MessageDTO>>(messages);
            return messagesDto;
        }

        public async Task<IEnumerable<Message>> GetConversationAsync(Guid user1Id, Guid user2Id)
        {
            var user1 = await _userRepository.GetAsync(user1Id);
            var user2 = await _userRepository.GetAsync(user2Id);

            if (user1 == null || user2 == null)
                throw new Exception($"User with id {user1Id} or/and {user2Id} does not exist.");

            var messages = await _messageRepository.GetConversationAsync(user1, user2);

            return messages;
        }

        public async Task SendAsync(Guid senderId, Guid receiverId, string content)
        {
            var user1 = await _userRepository.GetAsync(senderId);
            var user2 = await _userRepository.GetAsync(receiverId);

            if (user1 == null || user2 == null)
                throw new Exception($"User with id {senderId} or/and {receiverId} does not exist.");

            if (user1.ID == user2.ID)
                throw new Exception($"User cannot send message to himself.");

            Message message = new Message(user1, user2, content);

            await _messageRepository.AddAsync(message);
        }

        public async Task SetAsDisplayedAsync(Guid id)
        {
            var message = await _messageRepository.GetAsync(id);

            if (message == null)
                throw new Exception($"Message with id {id} does not exist.");

            message.SetAsDisplayed();

            await _messageRepository.UpdateAsync(message);
        }

        public async Task RemoveAsync(Guid id)
        {
            var message = await _messageRepository.GetAsync(id);

            if (message == null)
                throw new Exception($"Message with id {id} does not exist.");

            await _messageRepository.RemoveAsync(message);
        }

        public async Task RemoveConversationAsync(Guid user1Id, Guid user2Id)
        {
            var user1 = await _userRepository.GetAsync(user1Id);
            var user2 = await _userRepository.GetAsync(user2Id);

            if (user1 == null || user2 == null)
                throw new Exception($"User with id {user1Id} or/and {user2Id} does not exist.");

            await _messageRepository.RemoveConversationAsync(user1, user2);
        }
    }
}
