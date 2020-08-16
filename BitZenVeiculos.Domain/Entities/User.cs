using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace BitZenVeiculos.Domain.Entities
{
   public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome completo é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome completo deve ter entre 3 a 100 caracteres")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [StringLength(100, ErrorMessage = "O e-mail deve ter no máximo 100 caracteres")]
        [EmailAddress(ErrorMessage = "O e-mail é inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(8, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 a 8")]
        public string Password { get; set; }
        public virtual ICollection<Vehicle> Veichles { get; set; }
    }
}
