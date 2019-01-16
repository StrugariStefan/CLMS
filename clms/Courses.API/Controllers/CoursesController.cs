using System;
using System.Collections.Generic;
using Courses.API.Filters;
using Courses.API.Helpers;
using Courses.API.Models;
using Courses.API.Repository.Read;
using Courses.API.Repository.Write;
using Microsoft.AspNetCore.Mvc;

namespace Courses.API.Controllers
{
    [Produces("application / json")]
    [Route("api/v1/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IReadCourseRepository _readCourseRepository;
        private readonly IWriteCourseRepository _writeCourseRepository;
        private readonly IMapper<Course, CourseDto, CourseCreateDto> _mapper;

        public CoursesController(IReadCourseRepository readCourseRepository, IWriteCourseRepository writeCourseRepository, IMapper<Course, CourseDto, CourseCreateDto> mapper)
        {
            _readCourseRepository = readCourseRepository;
            _writeCourseRepository = writeCourseRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Return all courses.
        /// </summary>
        [HttpGet]
        [AuthFilter]
        [ProducesResponseType(200)]
        public ActionResult<IReadOnlyList<CourseDto>> Get()
        {
            return Ok(_mapper.EntityCollectionToDtoCollection(_readCourseRepository.GetAll()));
        }

        /// <summary>
        /// Obtains course by id.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Specified course</response>
        /// <response code="404">If course id doesn't exists</response>
        [HttpGet("{id}", Name = "GetByCourseId")]
        [AuthFilter]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<CourseDto> Get(Guid id)
        {
            if (!_readCourseRepository.Exists(id))
            {
                return NotFound();
            }

            return Ok(_mapper.EntityToDto(_readCourseRepository.GetById(id)));
        }

        /// <summary>
        /// Obtains course by name.
        /// </summary>
        /// <param name="name"></param>
        /// <response code="200">Specified course</response>
        /// <response code="404">If course name doesn't exists</response>
        [HttpGet("{name}", Name = "GetByCourseName")]
        [AuthFilter]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<CourseDto> GetByName(string name)
        {
            Course course = _readCourseRepository.GetByName(name);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(_mapper.EntityToDto(course));
        }

        /// <summary>
        /// Creates a new course.
        /// </summary>
        /// <response code="201">The created course</response>
        /// <response code="400">If courseDto is null, model is not valid or name already exists</response>
        [HttpPost]
        [AuthFilter, RoleFilter]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<Course> Post([FromBody] CourseCreateDto courseCreateDto)
        {
            if (courseCreateDto == null)
            {
                return BadRequest();
            }

            this.HttpContext.Items.TryGetValue("UserId", out var userId);
            courseCreateDto.CreatedBy = Guid.Parse(userId.ToString());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_readCourseRepository.GetByName(courseCreateDto.Name) != null)
            {
                return BadRequest("Name of Course already exists.");
            }

            Course course = _mapper.DtoToEntity(courseCreateDto);

            _writeCourseRepository.Create(course);
            _writeCourseRepository.SaveChanges();

            return CreatedAtRoute("GetByCourseId", new { id = course.Id }, course);
        }

        /// <summary>
        /// Deletes a specific Course.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Course has been deleted</response>
        /// <response code="404">If course id is not found</response>
        [HttpDelete("{id}")]
        [AuthFilter, RoleFilter, OwnerFilter]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult<Course> Delete(Guid id)
        {
            if (!_writeCourseRepository.Exists(id))
            {
                return NotFound();
            }

            _writeCourseRepository.Delete(id);
            _writeCourseRepository.SaveChanges();

            return NoContent();
        }
    }
}
