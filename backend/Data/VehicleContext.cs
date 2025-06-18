using Microsoft.EntityFrameworkCore;
using VehicleApi.Models;

namespace VehicleApi.Data
{
    public class VehicleContext : DbContext
    {
        public VehicleContext(DbContextOptions<VehicleContext> options) : base(options) { }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<VehicleEquipment> VehicleEquipments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().HasQueryFilter(v => !v.IsDeleted);

            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Brand)
                .WithMany(b => b.Vehicles)
                .HasForeignKey(v => v.BrandId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.Equipment);
                

            // Create some brands
            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, Name = "Volvo" },
                new Brand { Id = 2, Name = "Ford" },
                new Brand { Id = 3, Name = "Tesla" },
                new Brand { Id = 4, Name = "Toyota" },
                new Brand { Id = 5, Name = "BMW" }
            );

            // Create some vehicle equipment
            modelBuilder.Entity<VehicleEquipment>().HasData(
                new VehicleEquipment { Id = 1, Name = "Parking Sensor" },
                new VehicleEquipment { Id = 2, Name = "Rear view window" },
                new VehicleEquipment { Id = 3, Name = "Leather Seats" },
                new VehicleEquipment { Id = 4, Name = "Sunroof" },
                new VehicleEquipment { Id = 5, Name = "Bluetooth" }
            );

            // Create some vehicles
            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle { Id = 1, VehicleIdentificationNumber = 100213, Model = "V70", LicensePlate = "ABC123", Year = 2020, BrandId = 1 },
                new Vehicle { Id = 2, VehicleIdentificationNumber = 3231, Model = "Focus", LicensePlate = "CDE123",Year = 2022, BrandId = 2 },
                new Vehicle { Id = 3, VehicleIdentificationNumber = 6563, Model = "Model S", LicensePlate = "FGH123",Year = 2021, BrandId = 3 },
                new Vehicle { Id = 4, VehicleIdentificationNumber = 323122, Model = "Camry", LicensePlate = "HJK123",Year = 2019, BrandId = 4 },
                new Vehicle { Id = 5, VehicleIdentificationNumber = 4242, Model = "X5", LicensePlate = "KLM123",Year = 2021, BrandId = 5 }
            );

        }
    }
}