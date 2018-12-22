using System.Collections.Generic;

namespace Notifications.API.Helpers
{
    public interface IMapper<TE, TD>
    {
        TE DtoToEntity(TD dto);
        TD EntityToDto(TE entity);
        IEnumerable<TD> EntityCollectionToDtoCollection(IEnumerable<TE> entities);
    }
}
