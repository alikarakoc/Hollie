using Application.Concrete;
using Application.Dtos;
using AutoMapper;

namespace api.Mappers
{
    public class AgencyProfile : Profile
    {
        public AgencyProfile()
        {
            CreateMap<AgencyDto, Agency>()
                .ForMember(destination => destination.Name,
                operation => operation.MapFrom(source => source.Name))
                .ForMember(destination => destination.Code,
                operation => operation.MapFrom(source => source.Code))
                .ForMember(destination => destination.Address,
                operation => operation.MapFrom(source => source.Address))
                 .ForMember(destination => destination.Phone,
                operation => operation.MapFrom(source => source.Phone))
                .ForMember(destination => destination.Email,
                operation => operation.MapFrom(source => source.Email))
                 .ForMember(destination => destination.MarketList,
                operation => operation.MapFrom(source => source.MarketList));











        }
    }
}
