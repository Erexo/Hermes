using Hermes.Infrastructure.Commands;
using Hermes.Infrastructure.Commands.Messages;
using Hermes.Infrastructure.Services;
using System.Threading.Tasks;

namespace Hermes.Infrastructure.Handlers.Messages
{
    public class SendMessageHandler : ICommandHandler<SendMessage>
    {
        private readonly IMessageService _messageService;

        public SendMessageHandler(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task HandleAsync(SendMessage command)
        {
            await _messageService.SendAsync(command.ID, command.InterlocutorId, command.Content);
        }
    }
}
