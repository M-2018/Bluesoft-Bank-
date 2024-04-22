using BluesoftBank.Models;
using Microsoft.EntityFrameworkCore;

namespace BluesoftBank.Data
{
    public class CuentaDbContext : DbContext
    {
        public CuentaDbContext(DbContextOptions<CuentaDbContext> options) : base(options)
        {
        }

        //DbSet
        public DbSet<Cuenta> Cuentas { get; set; }
    }
}
