using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime;

namespace BitZenVeiculos.Domain.DTOs
{
    public class UserDTO
    {
        public class LoginRequestDTO
        {
            [Required(ErrorMessage = "O e-mail é obrigatório")]
            public string Email { get; set; }
            [Required(ErrorMessage = "A senha obrigatória")]
            public string Password { get; set; }
        }


        public class LoginResponseDTO
        {
            public Guid Id { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string token { get; set; }
        }
    }
}
