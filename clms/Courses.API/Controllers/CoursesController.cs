namespace Courses.API.Controllers
{
    using System;
    using System.Collections.Generic;

    using CLMS.Common;

    using Courses.API.Helpers;
    using Courses.API.Models;
    using Courses.API.Repository.Read;
    using Courses.API.Repository.Write;

    using Microsoft.AspNetCore.Mvc;

    [Produces("application/json")]
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
        /// <response code="400">If User has no permissions.</response>
        /// <response code="401">Token Unauthorized.</response>
        /// <response code="404">If course id is not found</response>
        [HttpDelete("{id}")]
        [AuthFilter, RoleFilter]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public ActionResult<Course> Delete(Guid id)
        {
            if (!_readCourseRepository.Exists(id))
            {
                return NotFound();
            }

            this.HttpContext.Items.TryGetValue("UserId", out var userId);

            if (!_readCourseRepository.GetOwnerById(id).ToString().Equals(userId))
            {
                return BadRequest("You dont have owner privileges for this course.");
            }

            _writeCourseRepository.Delete(id);
            _writeCourseRepository.SaveChanges();

            return NoContent();
        }
    }
}
