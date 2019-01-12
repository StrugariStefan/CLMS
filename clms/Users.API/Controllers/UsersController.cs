using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Users.API.Filters;
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
        private readonly IReadRepository _readRepository;
        private readonly IWriteRepository _writeRepository;
        private readonly IMapper _mapper;


        public UsersController(IReadRepository readRepository, IWriteRepository writeRepository,
            IMapper mapper)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtains an user by id.
        /// </summary>
        /// <param name="id"></param>
        [AuthFilter]
        [HttpGet("{id}", Name = "GetByUserId")]
        public ActionResult<UserDto> GetById(Guid id)
        {
            if (_readRepository.Exists(id) == false)
            {
                return NotFound();
            }

            return Ok(_mapper.EntityToDto(_readRepository.GetById(id)));
        }

        /// <summary>
        /// Obtains all users by role.
        /// </summary>
        [AuthFilter]
        [HttpGet("role/{role}", Name = "GetByRole")]
        public ActionResult<IReadOnlyList<UserDto>> GetByRole(int role)
        {
            return Ok(_mapper.EntityCollectionToDtoCollection(_readRepository.GetByRole(role)));
        }

        /// <summary>
        /// Returns all users.
        /// </summary>  
        [AuthFilter]
        [HttpGet]
        public ActionResult<IReadOnlyList<UserDto>> Get()
        {
            return Ok(_mapper.EntityCollectionToDtoCollection(_readRepository.GetAll()));
        }

        /// <summary>
        /// Deletes an user by id.
        /// </summary>
        [AuthFilter]
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

        /// <summary>
        /// Registers an user. Roles: 1 = student, 2 = teacher.
        /// </summary>  
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

            if (_readRepository.GetByEmail(userDto.Email) != null)
            {
                return BadRequest("The e-mail address is already used.");
            }

            var user = _mapper.DtoToEntity(userDto);

            _writeRepository.Create(user);
            _writeRepository.SaveChanges();

            return CreatedAtRoute("GetByUserId", new {id = user.Id}, user);
        }

        /// <summary>
        /// Checks if a user is registered.
        /// </summary>
        [HttpPost("registered")]
        public ActionResult Registered([FromBody] RegisteredRequest registeredRequest)
        {
            if (registeredRequest == null)
            {
                return BadRequest();
            }
            var email = registeredRequest.Email;
            var password = registeredRequest.Password.Hash();
            var user = _readRepository.GetByEmailAndPassword(email, password);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.Id.ToString());
        }
    }
}