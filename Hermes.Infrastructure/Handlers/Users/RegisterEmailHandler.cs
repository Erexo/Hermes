using System;
using System.Threading.Tasks;
using Hermes.Infrastructure.Commands;
using Hermes.Infrastructure.Commands.Users;
using Hermes.Infrastructure.Services;

namespace Hermes.Infrastructure.Handlers.Users
{
    public class RegisterEmailHandler : ICommandHandler<RegisterEmail>
    {
        private readonly IUserService _userService;

        public RegisterEmailHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(RegisterEmail command)
        {
            var user = await _userService.GetAsync(command.ID);

            if (user.Code != command.Code)
                throw new Exception("Registration code is invalid.");

            user.RegisterEmail();

            await _userService.UpdateAsync(user);
        }
    }
}
