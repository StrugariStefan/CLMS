using System.Collections.Generic;
using AutoMapper;
using Courses.API.Models;

namespace Courses.API.Helpers
{
    public class CourseMapper : IMapper<Course, CourseDto, CourseCreateDto>
    {
        private readonly MapperConfiguration _entityToDtoConfig;
        private readonly MapperConfiguration _dtoToEntityConfig;

        public CourseMapper()
        {
            _dtoToEntityConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CourseCreateDto, Course>()
                    .ConvertUsing(s => new Course(s.CreatedBy, s.Name, s.Description));
            });
            _entityToDtoConfig = new MapperConfiguration(entity => { entity.CreateMap<Course, CourseDto>(); });
        }

        public Course DtoToEntity(CourseCreateDto dto)
        {
            return _dtoToEntityConfig.CreateMapper().Map<Course>(dto);
        }

        public CourseDto EntityToDto(Course course)
        {
            return _entityToDtoConfig.CreateMapper().Map<CourseDto>(course);
        }

        public IEnumerable<CourseDto> EntityCollectionToDtoCollection(IEnumerable<Course> courses)
        {
            return _entityToDtoConfig.CreateMapper().Map<IEnumerable<Course>, IEnumerable<CourseDto>>(courses);
        }
    }
}
