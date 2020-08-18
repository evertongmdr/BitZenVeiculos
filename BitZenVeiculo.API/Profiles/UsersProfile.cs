using AutoMapper;
using BitZenVeiculos.Domain.Entities;
using static BitZenVeiculos.Domain.DTOs.UserDTO;

namespace BitZenVeiculos.API.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, LoginResponseDTO>().ReverseMap();
          
        }
    }
}
