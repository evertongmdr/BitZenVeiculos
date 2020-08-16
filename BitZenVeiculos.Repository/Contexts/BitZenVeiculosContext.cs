using BitZenVeiculos.Domain.Entities;
using BitZenVeiculos.Repository.Mapping;
using Microsoft.EntityFrameworkCore;

namespace BitZenVeiculos.Repository.Contexts
{
    public class BitZenVeiculosContext : DbContext
    {
        public BitZenVeiculosContext(DbContextOptions<BitZenVeiculosContext> options) : base(options)
        {

        }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<FuelSupply> FuelsSuplly { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new FuelSupplyMap());
            modelBuilder.ApplyConfiguration(new VehicleMap());

            base.OnModelCreating(modelBuilder);
        }

    }
}
