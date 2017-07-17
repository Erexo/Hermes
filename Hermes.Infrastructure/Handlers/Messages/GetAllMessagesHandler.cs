using Hermes.Infrastructure.Commands;
using Hermes.Infrastructure.Commands.Messages;
using Hermes.Infrastructure.Services;
using System.Threading.Tasks;

namespace Hermes.Infrastructure.Handlers.Messages
{
    public class GetAllMessagesHandler : ICommandHandler<GetAllMessages>
    {
        private readonly IMessageService _messageService;

        public GetAllMessagesHandler(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task HandleAsync(GetAllMessages command)
        {
            var messages = await _messageService.GetUserAsync(command.ID);

            command.Messages = messages;
        }
    }
}
