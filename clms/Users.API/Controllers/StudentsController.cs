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
        private readonly ReadStudentRepository _readRepository;
        private readonly IWriteRepository<Student> _writeRepository;
        private readonly IMapper<Student, StudentDto> _mapper;
        private readonly IMapper<Student, StudentCreateDto> _createMapper;

        public StudentsController(ReadStudentRepository readRepository, IWriteRepository<Student> writeRepository, IMapper<Student, StudentDto> mapper, IMapper<Student, StudentCreateDto> createMapper)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _mapper = mapper;
            _createMapper = createMapper;
        }

        /// <summary>
        /// Returns all students.
        /// </summary> 
        [HttpGet]
        public ActionResult<IReadOnlyList<StudentDto>> Get()
        {
            return Ok(_mapper.EntityCollectionToDtoCollection(_readRepository.GetAll()));
        }

        /// <summary>
        /// Returns all students belonging to a group.
        /// </summary> 
        [HttpGet("/group/{group}", Name = "GetByGroup")]
        public ActionResult<IReadOnlyList<StudentDto>> GetByGroup(string group)
        {
            return Ok(_mapper.EntityCollectionToDtoCollection(_readRepository.GetByGroup(group)));

        }

        /// <summary>
        /// Returns all students belonging to a year.
        /// </summary> 
        [HttpGet("/year/{year}", Name = "GetByYear")]
        public ActionResult<IReadOnlyList<StudentDto>> GetByYear(int year)
        {
            return Ok(_mapper.EntityCollectionToDtoCollection(_readRepository.GetByYear(year)));
        }


    }
}