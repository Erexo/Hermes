using System;
using System.Threading.Tasks;
using Hermes.Infrastructure.Commands;
using Hermes.Infrastructure.Commands.Users;
using Hermes.Infrastructure.Services;

namespace Hermes.Infrastructure.Handlers.Users
{
    public class ChangePasswordHandler : ICommandHandler<ChangePassword>
    {
        private readonly IUserService _userService;
        private readonly IEncrypter _encrypter;

        public ChangePasswordHandler(IUserService userService, IEncrypter encrypter)
        {
            _userService = userService;
            _encrypter = encrypter;
        }

        public async Task HandleAsync(ChangePassword command)
        {
            var user = await _userService.GetAsync(command.ID);
            var hash = _encrypter.GetHash(command.OldPassword, user.Salt);

            if (hash != user.Password)
                throw new Exception($"Passwords does not match.");

            var newPassword = _encrypter.GetHash(command.NewPassword, user.Salt);
            user.SetPassword(newPassword, user.Salt);
            await _userService.UpdateAsync(user);
        }
    }
}
