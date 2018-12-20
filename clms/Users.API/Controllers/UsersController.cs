﻿using System;
using System.Collections.Generic;
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
        private readonly IReadRepository<User> _readRepository;
        private readonly IWriteRepository<User> _writeRepository;
        private readonly IMapper<User, UserDto> _mapper;
        private readonly IMapper<User, UserCreateDto> _createMapper;


        public UsersController(IReadRepository<User> readRepository, IWriteRepository<User> writeRepository, IMapper<User, UserDto> mapper, IMapper<User, UserCreateDto> createMapper)
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
        /// Registers an user.
        /// </summary>  
        [HttpPost]
        public ActionResult<User> Create([FromBody] UserCreateDto userCreateDto)
        {
            if (userCreateDto == null)
            {
                return BadRequest();
            }

            Mapper.Initialize(cfg => {
                cfg.CreateMap<UserCreateDto, User>().ConvertUsing(s => new User(s.Name, s.Email, s.Phone, s.Password));
            });

            User user = Mapper.Map<UserCreateDto, User>(userCreateDto);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _writeRepository.Create(user);
            _writeRepository.SaveChanges();

            return CreatedAtRoute("GetByUserId", new { id = user.Id }, user);
        }
    }
}
