using BitZenVeiculos.Domain.Enums;
using BitZenVeiculos.Domain.Helpers.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace BitZenVeiculos.Domain.Entities
{
    public class Vehicle
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O ano é obrigatório")]
        [RangeUntilCurrentYear(1900, ErrorMessage = "O ano inserido é inválido")]
        public int Year { get; set; }

        [Required(ErrorMessage = "A placa é obrigatório")]
        [RegularExpression("/^[a-zA-Z]{3}[0-9]{4}$/", ErrorMessage = "A placa inserida é inválida")]
        public string LicensePlate { get; set; }

        [Required(ErrorMessage = "A quilometragem é obrigatória")]
        [Range(0, 2000000, ErrorMessage = "A quilometragem deve estar entre o intervalo de 0 a 2.000.000 de quilometros")]
        public decimal Mileage { get; set; }
        public string UrlPhoto { get; set;}

        [Required(ErrorMessage = "O Tipo do veículo é obrigatório")]
        public VehicleType VehicleType { get; set; }

        [Required(ErrorMessage = "O tipo de combustível é obrigatório")]
        public FuelType FuelType { get; set; }
        
        public virtual Model Model { get; set; }
        public virtual Make Make { get; set; }
        public virtual User ResponsibleUser { get; set; }
    }
}
