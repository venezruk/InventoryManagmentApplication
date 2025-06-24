using Microsoft.EntityFrameworkCore;

namespace InventoryManagmentApplication
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Warehouses> Warehouses { get; set; }

        public DbSet<Materials> Materials { get; set; }

        public DbSet<Routes> Routes { get; set; }

        public DbSet<Suppliers> Suppliers { get; set; }

        public DbSet<Products> Products { get; set; }

        public DbSet<Orders> Orders { get; set; }

        public DbSet<TransferOrdersShipment> TransferOrdersShipment { get; set; }

        public DbSet<ShipmentDocuments> ShipmentDocuments { get; set; }

        public DbSet<TransferOrdersOnTheWay> TransferOrdersOnTheWay { get; set; }

        public DbSet<OnTheWayDocuments> OnTheWayDocuments { get; set; }

        public DbSet<TransferOrdersAcceptance> TransferOrdersAcceptance { get; set; }

        public DbSet<AcceptanceDocuments> AcceptanceDocuments { get; set; }

        public DbSet<Processes> Processes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseJet(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=InventoryManagment.accdb;");
        }
    }
}
