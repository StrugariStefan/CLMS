using System;

namespace Users.API.Models
{
    public class User
    {
        private User()
        {
            //EF
        }

        public User(string name, string email, string phone)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Phone = phone;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
    }
}
