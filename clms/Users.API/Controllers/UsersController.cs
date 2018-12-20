using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using AutoMapper;
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
        private readonly ReadUserRepository _readRepository;
        private readonly IWriteRepository<User> _writeRepository;
        private readonly IMapper<User, UserDto> _mapper;
        private readonly IMapper<User, UserCreateDto> _createMapper;


        public UsersController(ReadUserRepository readRepository, IWriteRepository<User> writeRepository,
            IMapper<User, UserDto> mapper, IMapper<User, UserCreateDto> createMapper)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _mapper = mapper;
            _createMapper = createMapper;
        }

        /// <summary>
        /// Obtains an user by id.
        /// </summary>
        /// <param name="id"></param> 
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
        [HttpGet("/role/{role}", Name = "GetByRole")]
        public ActionResult<IReadOnlyList<UserDto>> GetByRole(int role)
        {
            return Ok(_mapper.EntityCollectionToDtoCollection(_readRepository.GetByRole(role)));
        }

        /// <summary>
        /// Returns all users.
        /// </summary>  
        [HttpGet]
        public ActionResult<IReadOnlyList<UserDto>> Get()
        {
            return Ok(_mapper.EntityCollectionToDtoCollection(_readRepository.GetAll()));
        }

        /// <summary>
        /// Deletes an user by id.
        /// </summary>  
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
        public ActionResult<User> Create([FromBody] UserCreateDto userCreateDto)
        {
            if (userCreateDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_readRepository.GetByEmail(userCreateDto.Email) != null)
            {
                return BadRequest("The e-mail address is already used.");
            }


            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserCreateDto, User>().ConvertUsing(s => new User(s.Name, s.Email, s.Phone, Hash(s.Password), s.Role));
            });

            User user = config.CreateMapper().Map<User>(userCreateDto);

            _writeRepository.Create(user);
            _writeRepository.SaveChanges();

            return CreatedAtRoute("GetByUserId", new {id = user.Id}, user);
        }

        private string Hash(string password)
        {
            byte[] hash = SHA256.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            string hashedPassword = System.Text.Encoding.UTF8.GetString(hash);

            return hashedPassword;
        }
    }
}