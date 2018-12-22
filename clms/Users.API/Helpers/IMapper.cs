using System.Collections.Generic;
using Users.API.Models;

namespace Users.API.Helpers
{
    public interface IMapper
    {
        User DtoToEntity(UserDto dto);
        UserDto EntityToDto(User user);
        IEnumerable<UserDto> EntityCollectionToDtoCollection(IEnumerable<User> users);
    }
}