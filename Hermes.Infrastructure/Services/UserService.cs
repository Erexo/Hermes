using System;
using System.Threading.Tasks;
using Hermes.Core.Domain;
using Hermes.Core.Repositories;
using Hermes.Infrastructure.DTO;
using AutoMapper;

namespace Hermes.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEncrypter _encrypter;

        public UserService(IUserRepository userRepository, IMapper mapper, 
            IEncrypter encrypter)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _encrypter = encrypter;
        }

        public async Task<User> GetAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);

            if (user == null)
                throw new Exception($"User with id {id} does not exist.");
            
            return user;
        }

        public async Task<User> GetAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null)
                throw new Exception($"User with username '{username}' does not exist.");
            
            return user;
        }

        public async Task<UserDTO> GetDTOAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);

            if (user == null)
                throw new Exception($"User with id {id} does not exist.");

            var userDto = _mapper.Map<UserDTO>(user);
            return userDto;
        }

        public async Task<UserDTO> GetDTOAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null)
                throw new Exception($"User with username '{username}' does not exist.");

            var userDto = _mapper.Map<UserDTO>(user);
            return userDto;
        }

        public async Task<Guid> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null)
                throw new Exception($"Invalid credentials.");
            
            var hash = _encrypter.GetHash(password, user.Salt);

            if(user.Password != hash)
                throw new Exception($"Invalid credentials.");

            return user.ID;
        }

        public async Task RegisterAsync(Guid userId, string username, string password, string email)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user != null)
                throw new Exception($"User with id {userId} does already exist.");

            user = await _userRepository.GetByUsernameAsync(username);
            if (user != null)
                throw new Exception($"User with username '{username}' does already exist.");

            user = await _userRepository.GetByEmailAsync(email);
            if (user != null)
                throw new Exception($"User with email '{email}' does already exist.");

            if (password.Length < 6 || password.Length > 32)
                throw new Exception("Password must be between 6 and 32 characters.");

            var salt = _encrypter.GetSalt(password);
            var hash = _encrypter.GetHash(password, salt);

            var newUser = new User(userId, username, hash, salt, email);

            await _userRepository.AddAsync(newUser);
        }

        public async Task UpdateAsync(User updatedUser)
        {
            var user = await _userRepository.GetAsync(updatedUser.ID);
            if (user == null)
                throw new Exception($"User with id {updatedUser.ID} does not exist.");

            await _userRepository.UpdateAsync(updatedUser);
        }
    }
}
