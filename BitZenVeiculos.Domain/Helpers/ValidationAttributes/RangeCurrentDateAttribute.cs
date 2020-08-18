using System;
using System.ComponentModel.DataAnnotations;

namespace BitZenVeiculos.Domain.Helpers.ValidationAttributes
{
    public class RangeCurrentDateAttribute : RangeAttribute
    {
        public RangeCurrentDateAttribute()
        : base(typeof(DateTime), "01/01/1900", DateTime.Now.ToString()) { }
    }
}
