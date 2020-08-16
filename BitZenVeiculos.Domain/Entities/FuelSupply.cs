using BitZenVeiculos.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace BitZenVeiculos.Domain.Entities
{

    public class FuelSupply
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A quilometragem do abastecimento é obrigatório")]
        [Range(1, 2000000, ErrorMessage = "A quilometragem do abastecimento deve estar entre o intervalo de 1 a 2.000.000 de quilometros")]
        public decimal SupplyedMileage { get; set;}

        [Required(ErrorMessage = "O litro(s) do abastecimento é obrigatório")]
        [Range(1.0, 100.0, ErrorMessage = "O litro(s) do abastecimento deve estar entre o intervalo de 1 a 100 litro(s)")]
        public decimal SupplyedLiters { get; set; }

        [Required(ErrorMessage = "O valor pago é obrigatório")]
        [Range(0.25, 1000, ErrorMessage = "O valor do pagamento deve estar entre o intervalo de R$ 0,25 a R$ 1.000,00")]
        public decimal ValuePay { get; set; }

        [Required(ErrorMessage = "A data do abastecimento é obrigatória")]
        public DateTime DateOfSupply { get; set; }

        [Required(ErrorMessage = "O posto abastecido é obrigatório")]
        [StringLength(100, ErrorMessage = "O posto abastecido deve ter no máximo 100 caracteres")]
        public string FuelStation { get; set; }

        [Required(ErrorMessage = "O tipo de combustível é obrigatório")]
        public FuelType FuelType { get; set; }
        public virtual User ResponsibleUser { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
