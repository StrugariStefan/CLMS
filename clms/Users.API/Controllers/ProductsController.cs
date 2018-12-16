using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Users.API.Models;
using Users.API.Repository;

namespace Users.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}", Name = "GetByProductId")]
        public ActionResult<Product> GetById(Guid id)
        {
            if (_repository.Exists(id) == false)
            {
                return NotFound();
            }

            return Ok(_repository.GetById(id));
        }

        [HttpGet]
        public ActionResult<IReadOnlyList<Product>> Get()
        {
            return Ok(_repository.GetAll());
        }

        [HttpDelete("{id}")]
        public ActionResult<Product> Delete(Guid id)
        {
            if (_repository.Exists(id) == false)
            {
                return NotFound();
            }

            _repository.Delete(id);
            _repository.SaveChanges();

            return NoContent();
        }


        [HttpPost]
        public ActionResult<Product> Create([FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest();
            }

            Product product = new Product(productDto.Name, productDto.Price, productDto.Pieces);
            _repository.Create(product);
            _repository.SaveChanges();

            return CreatedAtRoute("GetByProductId", new { id = product.Id }, product);
        }
    }
}