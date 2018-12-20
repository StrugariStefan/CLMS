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
        private readonly IMapper<Teacher, TeacherDto> _mapper;

        public TeachersController(IReadRepository<Teacher> readRepository, IMapper<Teacher, TeacherDto> mapper)
        {
            _readRepository = readRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all teachers.
        /// </summary> 
        [HttpGet]
        public ActionResult<IReadOnlyList<TeacherDto>> Get()
        {
            return Ok(_mapper.EntityCollectionToDtoCollection(_readRepository.GetAll()));
        }
    }
}