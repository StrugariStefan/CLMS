using System.Collections.Generic;
using AutoMapper;
using Gamification.API.Models;

namespace Gamification.API.Helpers
{
    public class AnswerMapper : IMapper<Answer, AnswerDto, AnswerCreateDto>
    {
        private readonly MapperConfiguration _entityToDtoConfig;
        private readonly MapperConfiguration _dtoToEntityConfig;

        public AnswerMapper()
        {
            _dtoToEntityConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AnswerCreateDto, Answer>()
                    .ConvertUsing(a => new Answer(a.ActualAnswer, a.CreatedBy, a.QuestionId));
            });
            _entityToDtoConfig = new MapperConfiguration(entity => { entity.CreateMap<Answer, AnswerDto>(); });
        }

        public Answer DtoToEntity(AnswerCreateDto dto)
        {
            return _dtoToEntityConfig.CreateMapper().Map<Answer>(dto);
        }

        public AnswerDto EntityToDto(Answer answer)
        {
            return _entityToDtoConfig.CreateMapper().Map<AnswerDto>(answer);
        }

        public IEnumerable<AnswerDto> EntityCollectionToDtoCollection(IEnumerable<Answer> answers)
        {
            return _entityToDtoConfig.CreateMapper().Map<IEnumerable<Answer>, IEnumerable<AnswerDto>>(answers);
        }
    }
}