using Hermes.Infrastructure.Commands;
using Hermes.Infrastructure.Commands.Messages;
using Hermes.Infrastructure.Services;
using System.Threading.Tasks;

namespace Hermes.Infrastructure.Handlers.Messages
{
    public class DeleteMessagesHandler : ICommandHandler<DeleteMessages>
    {
        private readonly IMessageService _messageService;

        public DeleteMessagesHandler(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task HandleAsync(DeleteMessages command)
        {
            await _messageService.RemoveConversationAsync(command.ID, command.InterlocutorID);
        }
    }
}
