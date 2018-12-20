using System.Collections.Generic;

namespace Users.API.Helpers
{
    public interface IMapper<E, D>
    {
        E DtoToEntity(D dto);
        D EntityToDto(E entity);
        IEnumerable<D> EntityCollectionToDtoCollection(IEnumerable<E> entities);
    }
}