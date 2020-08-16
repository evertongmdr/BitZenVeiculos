using System;
using System.ComponentModel.DataAnnotations;
namespace BitZenVeiculos.Domain.Entities
{
    public class Model
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O modelo é obrigatória")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O modelo deve ter entre 3 a 50 caracteres")]
        public string Description { get; set; }
    }
}
