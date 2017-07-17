using Hermes.Core.Domain;
using Hermes.Core.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hermes.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase _database;
        private IMongoCollection<User> Users => _database.GetCollection<User>("Users");

        public UserRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<User> GetAsync(Guid id)
            => await Users.AsQueryable().FirstOrDefaultAsync(o => o.ID == id);

        public async Task<User> GetByEmailAsync(string email)
            => await Users.AsQueryable().FirstOrDefaultAsync(o => o.Email == email);

        public async Task<User> GetByUsernameAsync(string username)
            => await Users.AsQueryable().FirstOrDefaultAsync(o => o.Username == username);

        public async Task<IEnumerable<User>> GetAllAsync()
            => await Users.AsQueryable().ToListAsync();

        public async Task AddAsync(User user)
            => await Users.InsertOneAsync(user);

        public async Task RemoveAsync(User user)
            => await Users.DeleteOneAsync(o => o.ID == user.ID);

        public async Task UpdateAsync(User user)
            => await Users.ReplaceOneAsync(o => o.ID == user.ID, user);
    }
}
