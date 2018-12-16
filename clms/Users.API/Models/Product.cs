using System;

namespace Users.API.Models
{
    public class Product
    {
        private Product()
        {
            //EF
        }

        public Product(string name, decimal price, int pieces)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Pieces = pieces;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Pieces { get; private set; }
    }
}
