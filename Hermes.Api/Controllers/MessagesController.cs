using Hermes.Infrastructure.Commands;
using Hermes.Infrastructure.Commands.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hermes.Api.Controllers
{
    public class MessagesController : BaseController
    {
        public MessagesController(ICommandDispatcher commandDispatcher)
            : base(commandDispatcher)
        {
        }

        [HttpGet]
        [Authorize]
        [Route("{InterlocutorId}")]
        public async Task<IActionResult> Conversation(GetConversation command)
        {
            await DispatchAsync(command);
            return Json(command.Messages);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AllMessages(GetAllMessages command)
        {
            await DispatchAsync(command);
            return Json(command.Messages);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete([FromBody]DeleteMessages command)
        {
            await DispatchAsync(command);
            return NoContent();
        }

        [HttpPost]
        [Authorize]
        [Route("send")]
        public async Task<IActionResult> Send([FromBody]SendMessage command)
        {
            await DispatchAsync(command);
            return Ok();
        }
    }
}
