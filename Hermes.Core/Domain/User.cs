using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.RegularExpressions;

namespace Hermes.Core.Domain
{
    public class User
    {
        [BsonId]
        public Guid ID { get; protected set; }
        public string Username { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string Email { get; protected set; }
        public string Code { get; protected set; }
        public bool Verified { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        private static readonly Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        public User(Guid id, string username, string password, string salt, string email)
        {
            ID = id;
            setUsername(username);
            SetPassword(password, salt);
            SetEmail(email);
            //Code
            CreatedAt = DateTime.Now;
        }

        protected User()
        {
        }

        private void setUsername(string username)
        {
            if (String.IsNullOrEmpty(username))
                throw new Exception("Username is invalid.");

            Username = username.ToLowerInvariant();
        }

        public void SetPassword(string password, string salt)
        {
            if (String.IsNullOrEmpty(password) || String.IsNullOrEmpty(salt))
                throw new Exception("Password is invalid.");

            Password = password;
            Salt = salt;
        }

        public void SetEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                throw new Exception("Email is invalid.");

            if (!emailRegex.IsMatch(email))
                throw new Exception("Email is invalid.");

            Email = email;
            Verified = false;
        }

        public void RegisterEmail()
        {
            Code = "";
            Verified = true;
        }
    }
}
