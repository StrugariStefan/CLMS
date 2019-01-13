using System;
using System.Collections.Generic;
using Gamification.API.Filters;
using Gamification.API.Helpers;
using Gamification.API.Models;
using Gamification.API.Repository.Read;
using Gamification.API.Repository.Write;
using Microsoft.AspNetCore.Mvc;

namespace Gamification.API.Controllers
{
    public class QuestionsController
    {

        [Produces("application / json")]
        [Route("api/v1/questions")]
        [ApiController]
        public class CoursesController : ControllerBase
        {
            private readonly IReadQuestionRepository _readQuestionRepository;
            private readonly IWriteQuestionRepository _writeQuestionRepository;
            private readonly IMapper<Question, QuestionDto, QuestionCreateDto> _mapper;

            public QuestionController(IReadQuestionRepository readQuestionRepository, IWriteQuestionRepository writeQuestionRepository, IMapper<Question, QuestionDto, QuestionCreateDto> mapper)
            {
                _readQuestionRepository = readQuestionRepository;
                _writeQuestionRepository = writeQuestionRepository;
                _mapper = mapper;
            }

            /// <summary>
            /// Return all questions.
            /// </summary>
            [HttpGet]
            [AuthFilter]
            [ProducesResponseType(200)]
            public ActionResult<IReadOnlyList<QuestionDto>> Get()
            {
                return Ok(_mapper.EntityCollectionToDtoCollection(_readQuestionRepository.GetAll()));
            }

            /// <summary>
            /// Obtains question by id.
            /// </summary>
            /// <param name="id"></param>
            /// <response code="200">Specified question</response>
            /// <response code="404">If question id doesn't exists</response>
            [HttpGet("{id}", Name = "GetByQuestionId")]
            [AuthFilter]
            [ProducesResponseType(200)]
            [ProducesResponseType(404)]
            public ActionResult<QuestionDto> Get(Guid id)
            {
                if (!_readQuestionRepository.Exists(id))
                {
                    return NotFound();
                }

                return Ok(_mapper.EntityToDto(_readQuestionRepository.GetById(id)));
            }

            /// <summary>
            /// Obtains question by type.
            /// </summary>
            /// <param name="name"></param>
            /// <response code="200">Specified question</response>
            /// <response code="404">If question name doesn't exists</response>
            [HttpGet("{type}", Name = "GetByQuestionType")]
            [AuthFilter]
            [ProducesResponseType(200)]
            [ProducesResponseType(404)]
            public ActionResult<QuestionDto> GetByQuestion(string type)
            {
                Question question= _readQuestionRepository.GetByType(type);

                if (question == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.EntityToDto(question));
            }

            /// <summary>
            /// Creates a new course.
            /// </summary>
            /// <response code="201">The created course</response>
            /// <response code="400">If courseDto is null, model is not valid or name already exists</response>
            [HttpPost]
            [AuthFilter]
            [ProducesResponseType(201)]
            [ProducesResponseType(400)]
            public ActionResult<Question> Post([FromBody] QuestionCreateDto courseCreateDto)
            {
                if (courseCreateDto == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (_readQuestionRepository.GetByName(courseQuestionDto.Name) != null)
                {
                    return BadRequest("Name of Course already exists.");
                }

                Question course = _mapper.DtoToEntity(courseCreateDto);

                _writeQuestionRepository.Create(course);
                _writeQuestionRepository.SaveChanges();

                return CreatedAtRoute("GetByCourseId", new { id = course.Id }, course);
            }

            /// <summary>
            /// Deletes a specific Course.
            /// </summary>
            /// <param name="id"></param>
            /// <response code="204">Course has been deleted</response>
            /// <response code="404">If course id is not found</response>
            [HttpDelete("{id}")]
            [AuthFilter]
            [ProducesResponseType(204)]
            [ProducesResponseType(404)]
            public ActionResult<Question> Delete(Guid id)
            {
                if (!_writeQuestionRepository.Exists(id))
                {
                    return NotFound();
                }

                _writeQuestionRepository.Delete(id);
                _writeQuestionRepository.SaveChanges();

                return NoContent();
            }
        }
    }

}
}
