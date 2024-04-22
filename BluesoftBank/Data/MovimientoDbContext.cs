using BluesoftBank.Models;
using Microsoft.EntityFrameworkCore;

namespace BluesoftBank.Data
{
    public class MovimientoDbContext : DbContext
    {
        public MovimientoDbContext(DbContextOptions<MovimientoDbContext> options) : base(options)
        {
        }

        //DbSet
        public DbSet<Movimiento> Movimientos { get; set; }
    }
}
