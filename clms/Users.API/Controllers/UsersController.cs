using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Users.API.Models;
using Users.API.Repository;

namespace Users.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}", Name = "GetByUserId")]
        public ActionResult<User> GetById(Guid id)
        {
            if (_repository.Exists(id) == false)
            {
                return NotFound();
            }


            return Ok(_repository.GetById(id));
        }

        [HttpGet]
        public ActionResult<IReadOnlyList<User>> Get()
        {
            return Ok(_repository.GetAll());
        }

        [HttpDelete("{id}")]
        public ActionResult<User> Delete(Guid id)
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
        public ActionResult<User> Create([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest();
            }

            User user = new User(userDto.Name);
            _repository.Create(user);
            _repository.SaveChanges();

            return CreatedAtRoute("GetByUserId", new { id = user.Id }, user);
        }
    }
}