using BitZenVeiculos.Domain.Enums;
using System;

namespace BitZenVeiculos.Domain.DTOs
{
    public class FuelSupplyDTO
    {

        public class FuelSupplyResponseDTO
        {
            public Guid Id { get; set; }
            public Guid ResponsibleUserId { get; set; }
            public Guid VehicleId { get; set; }
            public decimal SupplyedMileage { get; set; }
            public decimal SupplyedLiters { get; set; }
            public decimal ValuePay { get; set; }
            public DateTime DateOfSupply { get; set; }
            public string FuelStation { get; set; }
            public FuelType FuelType { get; set; }
           
        }
    }
}
