using BitZenVeiculos.Domain.Helpers.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace BitZenVeiculos.Domain.DTOs
{
    public class ReportDTO
    {
        public class ReportRequestDTO
        {
           
            [RangeCurrentDate(ErrorMessage = "A data de inicío é inválida")]
            public DateTime Start { get; set; }

            [RangeCurrentDate(ErrorMessage = "A data de fim é inválida")]
            public DateTime End { get; set; }
        }
    }
}
