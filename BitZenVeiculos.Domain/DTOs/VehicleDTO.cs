using BitZenVeiculos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitZenVeiculos.Domain.DTOs
{
    public class VehicleDTO
    {
        public class VehicleResponseDTO
        {
            public Guid? Id { get; set; }
            public Guid ModelId { get; set; }
            public Guid MakeId { get; set; }
            public Guid ResponsibleUserId { get; set; }
            public int Year { get; set; }          
            public string LicensePlate { get; set; }
            public decimal Mileage { get; set; }
            public string UrlPhoto { get; set; }
            public VehicleType VehicleType { get; set; }
            public FuelType FuelType { get; set; }

        }
    }
}
