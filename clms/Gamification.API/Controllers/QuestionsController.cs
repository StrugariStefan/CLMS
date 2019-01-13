using System;
using System.Collections.Generic;
using Gamification.API.Filters;
using Gamification.API.Helpers;
using Gamification.API.Models;
using Gamification.API.Repository.Read;
using Gamification.API.Repository.Write;
using Microsoft.AspNetCore.Mvc;
using Type = Gamification.API.Models.Type;

namespace Gamification.API.Controllers
{

        [Produces("application / json")]
        [Route("api/v1/questions")]
        [ApiController]
        public class QuestionsController : ControllerBase
        {
            private readonly IReadQuestionRepository _readQuestionRepository;
            private readonly IWriteQuestionRepository _writeQuestionRepository;
            private readonly IMapper<Question, QuestionDto, QuestionCreateDto> _mapper;

            public QuestionsController(IReadQuestionRepository readQuestionRepository, IWriteQuestionRepository writeQuestionRepository, IMapper<Question, QuestionDto, QuestionCreateDto> mapper)
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
            /// <response code="404">If question id doesn't exist</response>
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
            /// Obtains questions by type.
            /// </summary>
            /// <param name="type"></param>
            /// <response code="200">Specified questions</response>
            /// <response code="404">If there are no questions with the specified type</response>
            [HttpGet("{type}", Name = "GetByQuestionType")]
            [AuthFilter]
            [ProducesResponseType(200)]
            [ProducesResponseType(404)]
            public ActionResult<IReadOnlyList<QuestionDto>> GetByType(Type type)
            {
                
                IReadOnlyList<Question> questions = _readQuestionRepository.GetByType(type);

                if (questions == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.EntityCollectionToDtoCollection(questions));
              
            }

            /// <summary>
            /// Obtains questions by level of interest.
            /// </summary>
            /// <param name="levelOfInterest"></param>
            /// <response code="200">Specified questions</response>
            /// <response code="404">If questions with the specified level of interest don't exist</response>
            [HttpGet("{levelOfInterest}", Name = "GetByQuestionLevelOfInterest")]
            [AuthFilter]
            [ProducesResponseType(200)]
            [ProducesResponseType(404)]
            public ActionResult<IReadOnlyList<QuestionDto>> GetByLevelOfInterest(LevelOfInterest levelOfInterest)
            {

                IReadOnlyList<Question> questions = _readQuestionRepository.GetByLevelOfInterest(levelOfInterest);

                if (questions == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.EntityCollectionToDtoCollection(questions));

            }

            /// <summary>
            /// Creates a new question.
            /// </summary>
            /// <response code="201">The created question</response>
            /// <response code="400">If questionDto is null, model is not valid or name already exists</response>
            [HttpPost]
            [AuthFilter]
            [ProducesResponseType(201)]
            [ProducesResponseType(400)]
            public ActionResult<Question> Post([FromBody] QuestionCreateDto questionCreateDto)
            {
                if (questionCreateDto == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Question question = _mapper.DtoToEntity(questionCreateDto);

                _writeQuestionRepository.Create(question);
                _writeQuestionRepository.SaveChanges();

                return CreatedAtRoute("GetByQuestionId", new { id = question.Id }, question);
            }

            /// <summary>
            /// Deletes a specific Question.
            /// </summary>
            /// <param name="id"></param>
            /// <response code="204">Question has been deleted</response>
            /// <response code="404">If question id is not found</response>
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
