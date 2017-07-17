using Hermes.Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Hermes.Api.Controllers
{
    [Route("[controller]")]
    public abstract class BaseController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;
        protected Guid UserId
            => User?.Identity?.IsAuthenticated == true ? Guid.Parse(User.Identity.Name) : Guid.Empty;

        public BaseController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        protected async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if (command is IAuthenticatedCommand authCommand)
            {
                authCommand.ID = UserId;
            }
            await _commandDispatcher.DispatchAsync(command);
        }
    }
}
