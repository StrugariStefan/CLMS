using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Courses.API.Filters;
using Courses.API.Helpers;
using Courses.API.Models;
using Courses.API.Repository.Read;
using Courses.API.Repository.Write;
using Courses.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Courses.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/resourceFiles")]
    [ApiController]
    public class ResourceFilesController : ControllerBase
    {
        private readonly IReadResourceFileRepository _readResourceFileRepository;
        private readonly IWriteResourceFileRepository _writeResourceFileRepository;
        private readonly IReadCourseRepository _readCourseRepository;
        private readonly IMapper<ResourceFile, ResourceFileDto, ResourceFileCreateDto> _mapper;
        private readonly IFileStorageService _fileStorageService;

        public ResourceFilesController(IReadResourceFileRepository readResourceFileRepository, IWriteResourceFileRepository writeResourceFileRepository, IMapper<ResourceFile, ResourceFileDto, ResourceFileCreateDto> mapper, IReadCourseRepository readCourseRepository, IFileStorageService fileStorageService)
        {
            _readResourceFileRepository = readResourceFileRepository;
            _writeResourceFileRepository = writeResourceFileRepository;
            _mapper = mapper;
            _readCourseRepository = readCourseRepository;
            _fileStorageService = fileStorageService;
        }

        /// <summary>
        /// Return all resourceFiles.
        /// </summary>
        [HttpGet]
        [AuthFilter]
        [ProducesResponseType(200)]
        public ActionResult<IReadOnlyList<ResourceFileDto>> Get()
        {
            return Ok(_mapper.EntityCollectionToDtoCollection(_readResourceFileRepository.GetAll()));
        }


        /// <summary>
        /// Obtains resourceFile by id.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Specified resourceFile</response>
        /// <response code="404">If resourceFile id doesn't exists</response>
        [HttpGet("{id}", Name = "GetByResourceFileId")]
        [AuthFilter]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<ResourceFileDto> Get(Guid id)
        {
            if (!_readResourceFileRepository.Exists(id))
            {
                return NotFound();
            }

            return Ok(_mapper.EntityToDto(_readResourceFileRepository.GetById(id)));
        }

        /// <summary>
        /// Downloads a specified resource file
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Desired file is available for download</response>
        /// <response code="404">File not found</response>
        [HttpGet("download/{id}", Name = "GetFileById")]
        [AuthFilter]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces("application / octet-stream")]
        public async Task<ActionResult> Download(Guid id)
        {
            if (!_readResourceFileRepository.Exists(id))
            {
                return NotFound();
            }

            ResourceFile resourceFile = _readResourceFileRepository.GetById(id);

            MemoryStream stream = await _fileStorageService.DownloadFile(id, resourceFile.Name);
            
            return File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Octet, resourceFile.Name);
        }


        /// <summary>
        /// Creates a new resourceFile.
        /// </summary>
        /// <response code="201">The created resourceFile</response>
        /// <response code="400">If resourceFileDto is null, model is not valid or course id doesn't exist</response>
        /// <response code="401">Token Unauthorized.</response>
        [HttpPost]
        [AuthFilter, RoleFilter]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<ResourceFile>> Post([FromForm] ResourceFileCreateDto resourceFileCreateDto)
        {
            if (resourceFileCreateDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_readCourseRepository.Exists(resourceFileCreateDto.CourseId))
            {
                return BadRequest("Cannot assigne file to an non-existing course.");
            }

            this.HttpContext.Items.TryGetValue("UserId", out var userId);
            if (!_readCourseRepository.GetOwnerById(resourceFileCreateDto.CourseId).ToString().Equals(userId))
            {
                return BadRequest("You dont have owner privileges for this course.");
            }

            ResourceFile resource = _mapper.DtoToEntity(resourceFileCreateDto);

            await _fileStorageService.UploadFile(resource.Id, resourceFileCreateDto.Resource.OpenReadStream(), resourceFileCreateDto.Resource.FileName);

            _writeResourceFileRepository.Create(resource);
            _writeResourceFileRepository.SaveChanges();

            return CreatedAtRoute("GetByResourceFileId", new { id = resource.Id }, resource);
        }

        /// <summary>
        /// Deletes a specific resourceFile.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">ResourceFile has been deleted</response>
        /// <response code="400">If User has no permissions.</response>
        /// <response code="401">Token Unauthorized.</response>
        /// <response code="404">If resourceFile id is not found</response>
        [HttpDelete("{id}")]
        [AuthFilter, RoleFilter]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ResourceFile>> Delete(Guid id)
        {
            if (!_readResourceFileRepository.Exists(id))
            {
                return NotFound();
            }

            if (!_readResourceFileRepository.ExistsCourse(id))
            {
                return BadRequest("Cannot assigne file to an non-existing course.");
            }

            this.HttpContext.Items.TryGetValue("UserId", out var userId);
            var courseId = _readResourceFileRepository.GetCourseById(id);
            if (!_readCourseRepository.GetOwnerById(courseId).ToString().Equals(userId))
            {
                return BadRequest("You dont have owner privileges for this course.");
            }

            _writeResourceFileRepository.Delete(id);
            _writeResourceFileRepository.SaveChanges();

            await _fileStorageService.DeleteContainer(id);

            return NoContent();
        }
    }
}
