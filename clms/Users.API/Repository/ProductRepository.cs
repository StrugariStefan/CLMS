using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Users.API.Models;

namespace Users.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;

        public ProductRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Create(Product product)
        {
            _context.Products.Add(product);
        }

        public void Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            Product product = _context.Products.First(p => p.Id == id);
            _context.Products.Remove(product);
        }

        public Product GetById(Guid id)
        {
            return _context.Products.First(p => p.Id == id);
        }

        public IReadOnlyList<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool Exists(Guid id)
        {
            return _context.Products.Any(p => p.Id == id);
        }
    }
}
