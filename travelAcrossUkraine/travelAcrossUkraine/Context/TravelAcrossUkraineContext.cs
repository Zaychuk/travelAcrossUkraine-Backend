using Microsoft.EntityFrameworkCore;
using travelAcrossUkraine.WebApi.Entities;

namespace travelAcrossUkraine.WebApi.Context;

// EntityFrameworkCore\Add-Migration NameOfMigration

public class TravelAcrossUkraineContext : DbContext
{
    public TravelAcrossUkraineContext() : base()
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=TravelDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GeoPointEntity>(entity => 
        {
            entity.Property(p => p.CoordinateX).HasPrecision(20, 10);
            entity.Property(p => p.CoordinateY).HasPrecision(20, 10);
        });
    }
    public DbSet<GeoPointEntity> GeoPoints { get; set; }
    public DbSet<PolygonEntity> Polygons { get; set; }
}
