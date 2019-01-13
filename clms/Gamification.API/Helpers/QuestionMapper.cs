using System.Collections.Generic;
using AutoMapper;
using Gamification.API.Models;

namespace Gamification.API.Helpers
{
    public class QuestionMapper : IMapper<Question, QuestionDto, QuestionCreateDto>
    {
        private readonly MapperConfiguration _entityToDtoConfig;
        private readonly MapperConfiguration _dtoToEntityConfig;

        public QuestionMapper()
        {
            _dtoToEntityConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<QuestionCreateDto, Question>()
                    .ConvertUsing(q => new Question(q.CourseId, q.CreatedBy, q.ActualQuestion, q.LevelOfInterest,q.Type,q.TimeInMinutes));
            });
            _entityToDtoConfig = new MapperConfiguration(entity => { entity.CreateMap<Question, QuestionDto>(); });
        }

        public Question DtoToEntity(QuestionCreateDto dto)
        {
            return _dtoToEntityConfig.CreateMapper().Map<Question>(dto);
        }

        public QuestionDto EntityToDto(Question question)
        {
            return _entityToDtoConfig.CreateMapper().Map<QuestionDto>(question);
        }

        public IEnumerable<QuestionDto> EntityCollectionToDtoCollection(IEnumerable<Question> questions)
        {
            return _entityToDtoConfig.CreateMapper().Map<IEnumerable<Question>, IEnumerable<QuestionDto>>(questions);
        }
    }
}
