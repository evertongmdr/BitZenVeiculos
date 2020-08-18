using System;
using System.ComponentModel.DataAnnotations;

namespace BitZenVeiculos.Domain.Helpers.ValidationAttributes
{
    public class NotEmptyGuidAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;

            return ((Guid)value) == Guid.Empty ? false : true;
        }
    }
}
