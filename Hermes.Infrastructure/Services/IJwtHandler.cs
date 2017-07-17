using System;

namespace Hermes.Infrastructure.Services
{
    public interface IJwtHandler : IService
    {
        string getJwtToken(Guid id);
    }
}
