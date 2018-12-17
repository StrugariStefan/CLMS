using System;

namespace Users.API.Models
{
    public class User
    {
        private User()
        {
            //EF
        }

        public User(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}
