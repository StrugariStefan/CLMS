using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Courses.API.Models;

namespace Courses.API.Helpers
{
    public class ResourceFileMapper : IMapper<ResourceFile, ResourceFileDto, ResourceFileCreateDto>
    {
        private readonly MapperConfiguration _entityToDtoConfig;
        private readonly MapperConfiguration _dtoToEntityConfig;

        public ResourceFileMapper()
        {
            _dtoToEntityConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ResourceFileCreateDto, ResourceFile>()
                    .ConvertUsing(r => new ResourceFile(r.Resource.FileName, r.Description, r.Type, r.CourseId));
            });
            _entityToDtoConfig = new MapperConfiguration(entity => { entity.CreateMap<ResourceFile, ResourceFileDto>(); });
        }


        public ResourceFile DtoToEntity(ResourceFileCreateDto dto)
        {
            return _dtoToEntityConfig.CreateMapper().Map<ResourceFile>(dto);
        }

        public ResourceFileDto EntityToDto(ResourceFile entity)
        {
            return _entityToDtoConfig.CreateMapper().Map<ResourceFileDto>(entity);
        }

        public IEnumerable<ResourceFileDto> EntityCollectionToDtoCollection(IEnumerable<ResourceFile> entities)
        {
            return _entityToDtoConfig.CreateMapper().Map<IEnumerable<ResourceFile>, IEnumerable<ResourceFileDto>>(entities);
        }
    }
}
