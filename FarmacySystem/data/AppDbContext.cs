using FarmacySystem.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FarmacySystem.data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SaleMedicine> SaleMedicines { get; set; }
        public DbSet<SupplierMedicine> SupplierMedicines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsetting.json")
            .Build();

            optionsBuilder.UseNpgsql(config.GetConnectionString("PostgresConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Medicine>().ToTable("medicines");
            modelBuilder.Entity<Report>().ToTable("reports");
            modelBuilder.Entity<Sale>().ToTable("sales");
            modelBuilder.Entity<Stock>().ToTable("stocks");
            modelBuilder.Entity<Supplier>().ToTable("suppliers");

            // Garantir que a relação N:N seja única
            modelBuilder.Entity<SupplierMedicine>()
                .ToTable("suppliers_medicines")
                .HasIndex(sm => new { sm.MedicineId, sm.SupplierId })
                .IsUnique();

            modelBuilder.Entity<SaleMedicine>()
                .ToTable("sales_medicines")
                .HasIndex(sm => new { sm.StockId, sm.SaleId })
                .IsUnique();
        }
    }
}