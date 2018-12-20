using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Users.API.Helpers
{
    public class Mapper<E, D> : IMapper<E, D>
    {
        public MapperConfiguration Config { get; private set; }

        public Mapper()
        {
            Config = new MapperConfiguration(entity =>
            {
                 entity.CreateMap<E, D>();
            });
        }

        public E DtoToEntity(D dto)
        {
            return Config.CreateMapper().Map<E>(dto);
        }

        public D EntityToDto(E entity)
        {
            return Config.CreateMapper().Map<D>(entity);
        }

        public IEnumerable<D> EntityCollectionToDtoCollection(IEnumerable<E> entities)
        {
            return Config.CreateMapper().Map<IEnumerable<E>, IEnumerable<D>>(entities);
        }
    }
}
