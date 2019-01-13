using System.Collections.Generic;

namespace Gamification.API.Helpers
{
    public interface IMapper<TE, out TD, in TC>
    {
        TE DtoToEntity(TC dto);
        TD EntityToDto(TE entity);
        IEnumerable<TD> EntityCollectionToDtoCollection(IEnumerable<TE> entities);
    }
}
