using AutoMapper;
using BitZenVeiculos.Domain.Entities;
using static BitZenVeiculos.Domain.DTOs.VehicleDTO;

namespace BitZenVeiculos.API.Profiles
{
    public class VehiclesProfile: Profile
    {
        public VehiclesProfile()
        {
            CreateMap<Vehicle, VehicleResponseDTO>();
            CreateMap<VehicleResponseDTO, Vehicle>().ForMember(v => v.Id, opt => opt.Ignore());
        }
    }
}
