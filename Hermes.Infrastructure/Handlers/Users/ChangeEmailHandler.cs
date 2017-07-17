using System;
using System.Threading.Tasks;
using Hermes.Infrastructure.Commands;
using Hermes.Infrastructure.Commands.Users;
using Hermes.Infrastructure.Services;

namespace Hermes.Infrastructure.Handlers.Users
{
    public class ChangeEmailHandler : ICommandHandler<ChangeEmail>
    {
        private readonly IUserService _userService;

        public ChangeEmailHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(ChangeEmail command)
        {
            var user = await _userService.GetAsync(command.ID);

            if (command.OldEmail != user.Email)
                throw new Exception($"Emails does not match.");

            user.SetEmail(command.NewEmail);
            await _userService.UpdateAsync(user);
        }
    }
}
