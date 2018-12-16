using System;
using System.Collections.Generic;
using Users.API.Models;

namespace Users.API.Repository
{
    public interface IProductRepository
    {
        void Create(Product product);
        void Update(Product product);
        void Delete(Guid id);
        Product GetById(Guid id);
        IReadOnlyList<Product> GetAll();
        void SaveChanges();
        bool Exists(Guid id);
    }
}