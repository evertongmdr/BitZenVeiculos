using System;
using System.ComponentModel.DataAnnotations;

namespace BitZenVeiculos.Domain.Entities
{
    public class Make
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "A descrição deve ter entre 3 a 50 caracteres")]
        public string Description { get; set; }
    }
}
