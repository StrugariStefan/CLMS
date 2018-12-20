using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Users.API.Helpers;
using Users.API.Models;
using Users.API.Repository.Read;
using Users.API.Repository.Write;

namespace Users.API.Controllers
{
    [Route("api/v1/teachers")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly IReadRepository<Teacher> _readRepository;
        private readonly IWriteRepository<Teacher> _writeRepository;
        //todo (mapper pt teacher si teacher dto, stergeti cele cu useri de aici)
        private readonly IMapper<User, UserDto> _mapper;
        private readonly IMapper<User, UserCreateDto> _createMapper;

        public TeachersController(IReadRepository<Teacher> readRepository, IWriteRepository<Teacher> writeRepository, IMapper<User, UserDto> mapper, IMapper<User, UserCreateDto> createMapper)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _mapper = mapper;
            _createMapper = createMapper;
        }

        [HttpGet]
        public ActionResult<IReadOnlyList<TeacherDto>> Get()
        {
            return Ok(_mapper.EntityCollectionToDtoCollection(_readRepository.GetAll()));
        }
    }
}