using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public DbSet<ParkingSpot> ParkingSpots { get; set; }
    public DbSet<ParkingZone> ParkingZones { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ParkingSpot>()
            .HasOne<Vehicle>()
            .WithMany()
            .HasForeignKey(p => p.OccupiedByVehicleId)
            .IsRequired();

        modelBuilder.Entity<User>()
          .HasIndex(u => u.Username)
          .IsUnique();
    }
}
