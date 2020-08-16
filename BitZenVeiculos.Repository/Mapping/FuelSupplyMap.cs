using BitZenVeiculos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BitZenVeiculos.Repository.Mapping
{
    public class FuelSupplyMap : IEntityTypeConfiguration<FuelSupply>
    {
        public void Configure(EntityTypeBuilder<FuelSupply> builder)
        {
            builder.Property(fs => fs.SupplyedMileage).HasColumnType("decimal(9,2)");
            builder.Property(fs => fs.SupplyedLiters).HasColumnType("decimal(5,2)");
            builder.Property(fs => fs.ValuePay).HasColumnType("decimal(6,2)");

        }
    }
}
