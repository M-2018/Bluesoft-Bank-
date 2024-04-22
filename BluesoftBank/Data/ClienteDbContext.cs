using BluesoftBank.Models;
using Microsoft.EntityFrameworkCore;

namespace BluesoftBank.Data
{
    public class ClienteDbContext : DbContext
    {
        public ClienteDbContext(DbContextOptions<ClienteDbContext> options) : base(options)
        {
        }

        //DbSet
        public DbSet<Cliente> Clientes { get; set; }
    }
}
