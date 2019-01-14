using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Gamification.API.Models;

namespace Gamification.API.Helpers
{
    public class ScoreMapper : IMapper<Score, ScoreDto, ScoreDto>
    {
        private readonly MapperConfiguration _entityToDtoConfig;

        public ScoreMapper()
        {
            _entityToDtoConfig = new MapperConfiguration(entity => { entity.CreateMap<Score, ScoreDto>(); });
        }

        public Score DtoToEntity(ScoreDto dto)
        {
            throw new NotImplementedException();
        }

        public ScoreDto EntityToDto(Score entity)
        {
            return _entityToDtoConfig.CreateMapper().Map<ScoreDto>(entity);
        }

        public IEnumerable<ScoreDto> EntityCollectionToDtoCollection(IEnumerable<Score> entities)
        {
            return _entityToDtoConfig.CreateMapper().Map<IEnumerable<Score>, IEnumerable<ScoreDto>>(entities);
        }
    }
}
