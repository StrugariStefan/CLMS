using System;
using System.Collections.Generic;
using Courses.API.Helpers;
using Courses.API.Models;
using Courses.API.Repository.Read;
using Courses.API.Repository.Write;
using Microsoft.AspNetCore.Mvc;

namespace Courses.API.Controllers
{
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
        public ActionResult<IReadOnlyList<CourseDto>> Get()
        {
            return Ok(_mapper.EntityCollectionToDtoCollection(_readCourseRepository.GetAll()));
        }

        /// <summary>
        /// Obtains course by id.
        /// </summary>
        [HttpGet("{id}", Name = "GetByCourseId")]
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
        [HttpGet("{name}", Name = "GetByCourseName")]
        public ActionResult<CourseDto> GetByName(string name)
        {
            Course course = _readCourseRepository.GetByName(name);

            if ( course == null)
            {
                return NotFound();
            }

            return Ok(_mapper.EntityToDto(course));
        }

        /// <summary>
        /// Creates a new course.
        /// </summary>
        [HttpPost]
        public ActionResult<Course> Post([FromBody] CourseCreateDto courseCreateDto)
        {
            if (courseCreateDto == null)
            {
                return BadRequest();
            }

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

            return CreatedAtRoute("GetByCourseId", new {id = course.Id}, course);
        }

        /// <summary>
        /// Deletes a specific Course.
        /// </summary>
        [HttpDelete("{id}")]
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
