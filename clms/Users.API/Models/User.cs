using System;

namespace Users.API.Models
{
    public class User
    {
        private User()
        {
            //EF
        }

        public User(string name, string email, string phone, string password)
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Password { get; private set; }
    }
}
