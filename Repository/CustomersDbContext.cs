using Microsoft.EntityFrameworkCore;
using Angular_ASPNETCore_CustomersService.Models;

namespace Angular_ASPNETCore_CustomersService.Repository
{
    public class CustomersDbContext : DbContext
    {
        public DbSet<Barang> barang { get; set; }
        public CustomersDbContext (DbContextOptions<CustomersDbContext> options) : base(options) { }
    }
}