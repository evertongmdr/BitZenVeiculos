using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BitZenVeiculos.Domain.Enums
{
    public enum FuelType
    {
        [Description("Gasolina Comum")]
        GasolinaComum = 0,
        [Description("Gasolina Aditiva")]
        GasolinaAditiva = 1,
        [Description("Gasolina Premium")]
        GasolinaPremium = 2,
        [Description("Etanol Aditivo")]
        EtanolAditivo = 3,
        [Description("Gás Natural Veicular")]
        GasNaturalVeicular = 4,
        [Description("Diesel S-10")]
        DieselS10 = 5,
        [Description("Diesel Aditivo")]
        DieselAditivo = 6,
        [Description("Diesel Premium")]
        DieselPremium = 7
    }
}
