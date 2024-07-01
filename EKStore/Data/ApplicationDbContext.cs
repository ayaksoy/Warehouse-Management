using EKStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EKStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Driver> Driver { get; set; }
        public DbSet<ApplicationUser> Employee { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Shipment> Shipment { get; set; }
        public DbSet<ShipmentProduct> ShipmentProduct { get; set; }

    }
}
