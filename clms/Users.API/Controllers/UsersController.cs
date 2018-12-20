using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Users.API.Helpers;
using Users.API.Models;
using Users.API.Repository.Read;
using Users.API.Repository.Write;

namespace Users.API.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    { 
        private readonly IReadUserRepository _readRepository;
        private readonly IWriteUserRepository _writeRepository;
        private readonly IMapper<User, UserDto> _mapper;


        public UsersController(IReadUserRepository readRepository, IWriteUserRepository writeRepository, IMapper<User, UserDto> mapper)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetByUserId")]
        public ActionResult<UserDto> GetById(Guid id)
        {
            if (_readRepository.Exists(id) == false)
            {
                return NotFound();
            }

            return Ok(_mapper.EntityToDto(_readRepository.GetById(id)));
        }

        [HttpGet]
        public ActionResult<IReadOnlyList<UserDto>> Get()
        {
            return Ok(_mapper.EntityCollectionToDtoCollection(_readRepository.GetAll()));
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

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = _mapper.DtoToEntity(userDto);
            _writeRepository.Create(user);
            _writeRepository.SaveChanges();

            return CreatedAtRoute("GetByUserId", new { id = user.Id }, user);
        }
    }
}
