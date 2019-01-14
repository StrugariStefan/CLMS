using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gamification.API.Filters;
using Gamification.API.Helpers;
using Gamification.API.Models;
using Gamification.API.Repository.Read;
using Gamification.API.Repository.Write;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gamification.API.Controllers
{
    [Produces("application / json")]
    [Route("api/v1/scores")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly IReadScoreRepository _readScoreRepository;
        private readonly IWriteScoreRepository _writeScoreRepository;
        private readonly IMapper<Score, ScoreDto, ScoreDto> _mapper;

        public ScoresController(IReadScoreRepository readScoreRepository, IWriteScoreRepository writeScoreRepository, IMapper<Score, ScoreDto, ScoreDto> mapper)
        {
            _readScoreRepository = readScoreRepository;
            _writeScoreRepository = writeScoreRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Return all scores.
        /// </summary>
        [AuthFilter]
        [ProducesResponseType(200)]
        [HttpGet]
        public ActionResult<IReadOnlyList<ScoreDto>> Get()
        {
            return Ok(_mapper.EntityCollectionToDtoCollection(_readScoreRepository.GetAll()));
        }

        /// <summary>
        /// Return all scores by userId.
        /// </summary>
        /// <param name="userId"></param>
        [AuthFilter]
        [ProducesResponseType(200)]
        [HttpGet("user/{userId}", Name = "GetByUserId")]
        public ActionResult<IReadOnlyList<ScoreDto>> GetByUserId(Guid userId)
        {
            return Ok(_mapper.EntityCollectionToDtoCollection(_readScoreRepository.GetByUserId(userId)));
        }

        /// <summary>
        /// Return all scores by courseId.
        /// </summary>
        /// <param name="courseId"></param>
        [AuthFilter]
        [ProducesResponseType(200)]
        [HttpGet("course/{courseId}", Name = "GetByCourseId")]
        public ActionResult<IReadOnlyList<ScoreDto>> GetByCourseId(Guid courseId)
        {
            return Ok(_mapper.EntityCollectionToDtoCollection(_readScoreRepository.GetByCourseId(courseId)));
        }


        /// <summary>
        /// Obtains score by userId and courseId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="courseId"></param>
        /// <response code="200">Specified score</response>
        /// <response code="404">If score doesn't exist</response>
        [AuthFilter]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("user/{userId}/course/{courseId}", Name = "GetByUserAndCourseId")]
        public ActionResult<ScoreDto> Get(Guid userId, Guid courseId)
        {
            if (!_readScoreRepository.Exists(
                userId, courseId))
            {
                return NotFound();
            }

            return Ok(_mapper.EntityToDto(_readScoreRepository.GetByIds(userId, courseId)));
        }

        /// <summary>
        /// Adds points to score, creates new score if it doesn't exists.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="courseId"></param>
        /// <response code="204">Score updated</response>
        [AuthFilter]
        [ProducesResponseType(204)]
        [HttpPut("user/{userId}/course/{courseId}/addpoints")]
        public ActionResult<ScoreDto> Put(Guid userId, Guid courseId, [FromBody] AddedPoints addedPoints)
        {
            if (!_writeScoreRepository.Exists(
                userId, courseId))
            {
                _writeScoreRepository.Create(new Score(courseId, userId));
                _writeScoreRepository.SaveChanges();
            }

            _writeScoreRepository.AddPoints(userId, courseId, addedPoints.Points);
            _writeScoreRepository.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deletes a specific Score.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="courseId"></param>
        /// <response code="204">Score has been deleted</response>
        /// <response code="404">If score is not found</response>
        [AuthFilter]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [HttpDelete("user/{userId}/course/{courseId}")]
        public ActionResult<ScoreDto> Delete(Guid userId, Guid courseId)
        {
            if (!_writeScoreRepository.Exists(userId, courseId))
            {
                return NotFound();
            }

            _writeScoreRepository.Delete(
                userId, courseId);
            _writeScoreRepository.SaveChanges();

            return NoContent();
        }
    }
}
