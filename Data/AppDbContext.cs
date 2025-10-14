using Microsoft.EntityFrameworkCore;
using ESGInteligentes.Models;

namespace ESGInteligentes.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ConsumoEnergia> ConsumosEnergia { get; set; }
    }
}