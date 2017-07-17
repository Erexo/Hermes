using Hermes.Infrastructure.Commands;
using Hermes.Infrastructure.Commands.Users;
using Hermes.Infrastructure.Services;
using System;
using System.Threading.Tasks;

namespace Hermes.Infrastructure.Handlers.Users
{
    public class LoginUserHandler : ICommandHandler<LoginUser>
    {
        private readonly IUserService _userService;
        private readonly IJwtHandler _jwtHandler;

        public LoginUserHandler(IUserService userService, IJwtHandler jwtHandler)
        {
            _userService = userService;
            _jwtHandler = jwtHandler;
        }

        public async Task HandleAsync(LoginUser command)
        {
            var id = await _userService.LoginAsync(command.Username, command.Password);

            command.Jwt = _jwtHandler.getJwtToken(id);
        }
    }
}
