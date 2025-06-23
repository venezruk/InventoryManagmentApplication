using System.Windows.Media.Media3D;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagmentApplication
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Warehouses> Warehouses { get; set; } = null!;

        public DbSet<Materials> Materials { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseJet(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=InventoryManagment.accdb;");
        }
    }
}
