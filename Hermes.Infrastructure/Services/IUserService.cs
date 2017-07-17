using Hermes.Core.Domain;
using Hermes.Infrastructure.DTO;
using System;
using System.Threading.Tasks;

namespace Hermes.Infrastructure.Services
{
    public interface IUserService : IService
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string username);
        Task<UserDTO> GetDTOAsync(Guid id);
        Task<UserDTO> GetDTOAsync(string username);
        Task RegisterAsync(Guid userId, string username, string password, string email);
        Task<Guid> LoginAsync(string username, string password);
        Task UpdateAsync(User updatedUser);
    }
}
