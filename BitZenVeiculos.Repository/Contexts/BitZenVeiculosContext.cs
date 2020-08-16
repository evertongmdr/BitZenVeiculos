using BitZenVeiculos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BitZenVeiculos.Repository.Contexts
{
    public class BitZenVeiculosContext : DbContext
    {
        public BitZenVeiculosContext(DbContextOptions<BitZenVeiculosContext> options) : base(options)
        {

        }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
