using System.Collections.Generic;
using AutoMapper;
using Users.API.Models;

namespace Users.API.Helpers
{
    public class UserMapper : IMapper
    {
        public MapperConfiguration Config { get; private set; }

        public UserMapper()
        {
            Config = new MapperConfiguration(entity => { entity.CreateMap<User, UserDto>(); });
        }

        public User DtoToEntity(UserDto dto)
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto, User>()
                    .ConvertUsing(s => new User(s.Name, s.Email, s.Phone, s.Password.Hash(), s.Role));
            });

            User user = config.CreateMapper().Map<User>(dto);

            return user;
        }

        public UserDto EntityToDto(User user)
        {
            return Config.CreateMapper().Map<UserDto>(user);
        }

        public IEnumerable<UserDto> EntityCollectionToDtoCollection(IEnumerable<User> users)
        {
            return Config.CreateMapper().Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }
    }
}