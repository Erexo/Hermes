using Hermes.Infrastructure.Commands;
using Hermes.Infrastructure.Commands.Users;
using Hermes.Infrastructure.Services;
using System;
using System.Threading.Tasks;

namespace Hermes.Infrastructure.Handlers.Users
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IUserService _userService;

        public CreateUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(CreateUser command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), command.Username, command.Password, command.Email);
        }
    }
}
