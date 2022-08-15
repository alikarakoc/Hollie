using Application.Concrete;
using Application.Dtos;
using AutoMapper;

namespace api.Mappers
{
    public class ContractProfile : Profile
    {
        public ContractProfile()
        {
            CreateMap<ContractDto, Contract>()
                .ForMember(destination => destination.Name,
                operation => operation.MapFrom(source => source.Name))
                .ForMember(destination => destination.Code,
                operation => operation.MapFrom(source => source.Code))
                .ForMember(destination => destination.HotelId,
                operation => operation.MapFrom(source => source.HotelId))
                .ForMember(destination => destination.CurrencyId,
                operation => operation.MapFrom(source => source.CurrencyId))
                .ForMember(destination => destination.EnteredDate,
                operation => operation.MapFrom(source => source.EnteredDate))
                .ForMember(destination => destination.ExitDate,
                operation => operation.MapFrom(source => source.ExitDate))
                .ForMember(destination => destination.AgencyList,
                operation => operation.MapFrom(source => source.AgencyList))
                .ForMember(destination => destination.BoardList,
                operation => operation.MapFrom(source => source.BoardList))
                 .ForMember(destination => destination.MarketList,
                operation => operation.MapFrom(source => source.MarketList));











        }
    }
}
