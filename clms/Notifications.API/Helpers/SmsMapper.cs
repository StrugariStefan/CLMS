using System.Collections.Generic;
using AutoMapper;
using Notifications.API.Models.Dtos;
using Notifications.API.Models.Entities;

namespace Notifications.API.Helpers
{
    public class SmsMapper : IMapper<Sms, SmsDto>
    {
        public MapperConfiguration Config { get; private set; }

        public SmsMapper()
        {
            Config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Sms, SmsDto>();
            });
        }
        public Sms DtoToEntity(SmsDto smsDto)
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SmsDto, Sms>()
                    .ConvertUsing(s => new Sms(s.From, s.To, s.Message));
            });

            Sms sms = config.CreateMapper().Map<Sms>(smsDto);

            return sms;
        }

        public SmsDto EntityToDto(Sms sms)
        {
            return Config.CreateMapper().Map<SmsDto>(sms);
        }

        public IEnumerable<SmsDto> EntityCollectionToDtoCollection(IEnumerable<Sms> entities)
        {
            return Config.CreateMapper().Map<IEnumerable<Sms>, IEnumerable<SmsDto>>(entities);
        }
    }
}
