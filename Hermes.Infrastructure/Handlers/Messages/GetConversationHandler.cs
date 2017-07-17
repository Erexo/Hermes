using System;
using System.Threading.Tasks;
using Hermes.Infrastructure.Commands;
using Hermes.Infrastructure.Commands.Messages;
using Hermes.Infrastructure.Services;
using Hermes.Core.Domain;

namespace Hermes.Infrastructure.Handlers.Messages
{
    public class GetConversationHandler : ICommandHandler<GetConversation>
    {
        private readonly IMessageService _messageService;

        public GetConversationHandler(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task HandleAsync(GetConversation command)
        {
            var messages = await _messageService.GetConversationAsync(command.ID, command.InterlocutorID);

            foreach(Message message in messages)
            {
                if (!message.Displayed)
                    await _messageService.SetAsDisplayedAsync(message.ID);
            }

            command.Messages = messages;
        }
    }
}
