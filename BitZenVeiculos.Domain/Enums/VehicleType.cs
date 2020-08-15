using System.ComponentModel;

namespace BitZenVeiculos.Domain.Enums
{
    public enum VehicleType
    {
        Bicicleta = 0,
        Ciclomotor = 1,
        Motoneta = 2,
        Motocicleta = 3,
        Triciclo = 4,
        Quadriciclo = 5,
        [Description("Automóvel")]
        Automovel = 6,
        [Description("Microônibus")]
        Microonibus = 7,
        [Description("Ônibus")]
        Onibus = 8,
        Bonde = 9,
        [Description("Reboque ou semi-reboque")]
        ReboqueOuSemiReboque = 10,
        Charrete = 11
    }

}
