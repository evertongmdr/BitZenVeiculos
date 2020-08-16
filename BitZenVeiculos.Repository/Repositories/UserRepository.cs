using BitZenVeiculos.Domain.Contracts;
using BitZenVeiculos.Domain.Entities;
using BitZenVeiculos.Repository.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BitZenVeiculos.Domain.DTOs.UserDTO;

namespace BitZenVeiculos.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BitZenVeiculosContext _userContext;
        public UserRepository(BitZenVeiculosContext userContext)
        {
            _userContext = userContext;
        }
        public bool CreateUser(User user)
        {
            _userContext.Add(user);
            return Save();
        }
        public bool UpdateUser(User user)
        {
            _userContext.Add(user);
            return Save();
        }

        public async Task<User> GetUser(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException(nameof(userId));

            return await _userContext.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
        }

        public IEnumerable<User> GetUsers()
        {
            return _userContext.Users.AsNoTracking();
        }


        public IEnumerable<Vehicle> GetVehicleByUser(Guid userId)
        {
            return _userContext.Vehicles.AsNoTracking().Where(v => v.ResponsibleUser.Id == userId).ToList();
        }

        public bool UserExists(Guid userId)
        {
            return _userContext.Users.AsNoTracking().Any(a => a.Id == userId);
        }

        public async Task<bool> EmailExists(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            return await _userContext.Users.AsNoTracking().AnyAsync(a => a.Email.ToLower() == email.ToLower());
        }

        public User LoginUser(LoginRequestDTO user)
        {
            return _userContext.Users.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefault();
        }
        public bool Save()
        {
            int saved = _userContext.SaveChanges();
            return saved >= 0 ? true : false;
        }
    }
}
