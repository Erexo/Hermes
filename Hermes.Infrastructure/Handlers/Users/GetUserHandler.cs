using System;
using System.Threading.Tasks;
using Hermes.Infrastructure.Commands;
using Hermes.Infrastructure.Commands.Users;
using Hermes.Infrastructure.Services;

namespace Hermes.Infrastructure.Handlers.Users
{
    public class GetUserHandler : ICommandHandler<GetUser>
    {
        private readonly IUserService _userService;

        public GetUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(GetUser command)
        {
            var user = await _userService.GetDTOAsync(command.ID);
            command.User = user;
        }
    }
}
