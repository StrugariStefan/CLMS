namespace Gamification.API.Controllers
{
    using System;
    using System.Collections.Generic;

    using CLMS.Common.Filters;

    using Gamification.API.Helpers;
    using Gamification.API.Models;
    using Gamification.API.Repository.Read;
    using Gamification.API.Repository.Write;

    using Microsoft.AspNetCore.Mvc;

    [Produces("application/json")]
    [Route("api/v1/answers")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly IReadAnswerRepository _readAnswerRepository;
        private readonly IWriteAnswerRepository _writeAnswerRepository;
        private readonly IMapper<Answer, AnswerDto, AnswerCreateDto> _mapper;

        public AnswersController(IReadAnswerRepository readAnswerRepository, IWriteAnswerRepository writeAnswerRepository, IMapper<Answer, AnswerDto, AnswerCreateDto> mapper)
        {
            _readAnswerRepository = readAnswerRepository;
            _writeAnswerRepository = writeAnswerRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Return all answers.
        /// </summary>
        [HttpGet]
        [AuthFilter]
        [ProducesResponseType(200)]
        public ActionResult<IReadOnlyList<AnswerDto>> Get()
        {
            return Ok(_mapper.EntityCollectionToDtoCollection(_readAnswerRepository.GetAll()));
        }

        /// <summary>
        /// Obtains answer by id.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Specified answer</response>
        /// <response code="404">If answer id doesn't exist</response>
        [HttpGet("{id}", Name = "GetByAnswerId")]
        [AuthFilter]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<AnswerDto> Get(Guid id)
        {
            if (!_readAnswerRepository.Exists(id))
            {
                return NotFound();
            }

            return Ok(_mapper.EntityToDto(_readAnswerRepository.GetById(id)));
        }


        /// <summary>
        /// Creates a new answer.
        /// </summary>
        /// <response code="201">The created answer</response>
        /// <response code="400">If answerDto is null, model is not valid or name already exists</response>
        [HttpPost]
        [AuthFilter]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<Answer> Post([FromBody] AnswerCreateDto answerCreateDto)
        {
            if (answerCreateDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Answer answer = _mapper.DtoToEntity(answerCreateDto);

            _writeAnswerRepository.Create(answer);
            _writeAnswerRepository.SaveChanges();

            return CreatedAtRoute("GetByAnswerId", new { id = answer.Id }, answer);
        }

        /// <summary>
        /// Deletes a specific Answer.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Answer has been deleted</response>
        /// <response code="404">If answer id is not found</response>
        [HttpDelete("{id}")]
        [AuthFilter]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult<Answer> Delete(Guid id)
        {
            if (!_writeAnswerRepository.Exists(id))
            {
                return NotFound();
            }

            _writeAnswerRepository.Delete(id);
            _writeAnswerRepository.SaveChanges();

            return NoContent();
        }
    }

}


