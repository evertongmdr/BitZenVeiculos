using BitZenVeiculos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static BitZenVeiculos.Domain.DTOs.UserDTO;

namespace BitZenVeiculos.Domain.Contracts
{
    public interface IUserRepository
    {
        Task<User> GetUser(Guid userId);
        IEnumerable<Vehicle> GetVehicleByUser(Guid userId);
        Task<bool> UserExists(Guid userId);
        bool CreateUser(User user);
        User LoginUser(LoginRequestDTO user);
        Task<bool> EmailExists(string email);
        bool Save();
    }
}
