using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BitZenVeiculos.Domain.Entities
{
    public class Supply
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "A quilometragem do abastecimento é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "A A quilometragem do abastecimento deve ser maior que zero")]
        public int SupplyedMileage { get; set;}

        [Required(ErrorMessage = "O litro(s) do abastecimento é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O litro(s) do abastecimento deve ser maior que zero")]
        public int SupplyedLiters { get; set; }

    }
}
