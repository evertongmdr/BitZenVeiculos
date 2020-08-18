using System.ComponentModel;

namespace BitZenVeiculos.Domain.Enums
{
    public enum VehicleType
    {
        Bicicleta = 1,
        Ciclomotor = 2,
        Motoneta = 3,
        Motocicleta = 4,
        Triciclo = 5,
        Quadriciclo = 6,
        [Description("Automóvel")]
        Automovel = 7,
        [Description("Microônibus")]
        Microonibus = 8,
        [Description("Ônibus")]
        Onibus = 9,
        Bonde = 10,
        [Description("Reboque ou semi-reboque")]
        ReboqueOuSemiReboque = 11,
        Charrete = 12
    }

}
