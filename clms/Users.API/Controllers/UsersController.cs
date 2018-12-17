using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Users.API.Models;
using Users.API.Repository.Read;
using Users.API.Repository.Write;

namespace Users.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    { 
        private readonly IReadUserRepository _readRepository;
        private readonly IWriteUserRepository _writeRepository;


        public UsersController(IReadUserRepository readRepository, IWriteUserRepository writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        [HttpGet("{id}", Name = "GetByUserId")]
        public ActionResult<User> GetById(Guid id)
        {
            if (_readRepository.Exists(id) == false)
            {
                return NotFound();
            }


            return Ok(_readRepository.GetById(id));
        }

        [HttpGet]
        public ActionResult<IReadOnlyList<User>> Get()
        {
            return Ok(_readRepository.GetAll());
        }

        [HttpDelete("{id}")]
        public ActionResult<User> Delete(Guid id)
        {
            if (_writeRepository.Exists(id) == false)
            {
                return NotFound();
            }

            _writeRepository.Delete(id);
            _writeRepository.SaveChanges();

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
            _writeRepository.Create(user);
            _writeRepository.SaveChanges();

            return CreatedAtRoute("GetByUserId", new { id = user.Id }, user);
        }
    }
}
