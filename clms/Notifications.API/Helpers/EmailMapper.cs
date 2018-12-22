using System.Collections.Generic;
using AutoMapper;
using Notifications.API.Models.Dtos;
using Notifications.API.Models.Entities;

namespace Notifications.API.Helpers
{
    public class EmailMapper : IMapper<Email, EmailDto>
    {
        public MapperConfiguration Config { get; private set; }

        public EmailMapper()
        {
            Config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Email, EmailDto>();
            });
        }

        public Email DtoToEntity(EmailDto emailDto)
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmailDto, Email>()
                    .ConvertUsing(e => new Email(e.From, e.To, e.Cc, e.Bcc, e.Subject, e.Body));
            });

            Email email = config.CreateMapper().Map<Email>(emailDto);

            return email;
        }

        public EmailDto EntityToDto(Email email)
        {
            return Config.CreateMapper().Map<EmailDto>(email);
        }

        public IEnumerable<EmailDto> EntityCollectionToDtoCollection(IEnumerable<Email> emails)
        {
            return Config.CreateMapper().Map<IEnumerable<Email>, IEnumerable<EmailDto>>(emails);
        }
    }
}
