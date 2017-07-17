using System;

namespace Hermes.Infrastructure.Commands
{
    public interface IAuthenticatedCommand : ICommand
    {
        Guid ID { get; set; }
    }
}
