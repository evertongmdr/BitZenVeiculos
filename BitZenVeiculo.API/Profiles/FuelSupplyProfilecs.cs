using AutoMapper;
using BitZenVeiculos.Domain.Entities;
using static BitZenVeiculos.Domain.DTOs.FuelSupplyDTO;

namespace BitZenVeiculos.API.Profiles
{
    public class FuelSupplyProfilecs : Profile
    {
        public FuelSupplyProfilecs()
        {
            CreateMap<FuelSupply, FuelSupplyResponseDTO>();
            CreateMap<FuelSupplyResponseDTO, FuelSupply>().ForMember(v => v.Id, opt => opt.Ignore());
        }
    }
}
