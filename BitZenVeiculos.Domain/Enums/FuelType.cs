using System.ComponentModel;

namespace BitZenVeiculos.Domain.Enums
{
    public enum FuelType
    {
        [Description("Gasolina Comum")]
        GasolinaComum = 1,
        [Description("Gasolina Aditiva")]
        GasolinaAditiva = 2,
        [Description("Gasolina Premium")]
        GasolinaPremium = 3,
        [Description("Etanol Aditivo")]
        EtanolAditivo = 4,
        [Description("Gás Natural Veicular")]
        GasNaturalVeicular = 5,
        [Description("Diesel S-10")]
        DieselS10 = 6,
        [Description("Diesel Aditivo")]
        DieselAditivo = 7,
        [Description("Diesel Premium")]
        DieselPremium = 8
    }
}
