using BitZenVeiculos.Domain.Enums;
using BitZenVeiculos.Domain.Extensions;
using System;

namespace BitZenVeiculos.Domain
{
    class Program
    {
        static void Main(string[] args)
        {

            var result = EnumExtensions.GetEnumDescription(VehicleType.Automovel);
            Console.WriteLine(result);
        }
    }
}
