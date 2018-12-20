using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Users.API.Helpers;
using Users.API.Models;
using Users.API.Repository.Read;
using Users.API.Repository.Write;

namespace Users.API.Controllers
{
    [Route("api/v1/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IReadRepository<Student> _readRepository;
        private readonly IWriteRepository<Student> _writeRepository;
        //todo (mapper pt student si student dto, stergeti cele curente de aici cu useri, inclusiv in constructor)
        private readonly IMapper<User, UserDto> _mapper;
        private readonly IMapper<User, UserCreateDto> _createMapper;

        public StudentsController(IReadRepository<Student> readRepository, IWriteRepository<Student> writeRepository, IMapper<User, UserDto> mapper, IMapper<User, UserCreateDto> createMapper)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _mapper = mapper;
            _createMapper = createMapper;
        }

        [HttpGet]
        public ActionResult<IReadOnlyList<StudentDto>> Get()
        {
            return Ok(_mapper.EntityCollectionToDtoCollection(_readRepository.GetAll()));
        }

        [HttpGet("/group/{group}", Name = "GetByGroup")]
        public ActionResult<UserDto> GetByGroup(string group)
        {
            //TODO IMPLEMENT:ntoarceti totii studentii din grupa specificata ca paramateru
            return null;
        }

        [HttpGet("/year/{year}", Name = "GetByYear")]
        public ActionResult<UserDto> GetByYear(int year)
        {
            //TODO IMPLEMENT: intoarceti totii studentii din anul specificat ca paramateru
            return null;
        }


    }
}