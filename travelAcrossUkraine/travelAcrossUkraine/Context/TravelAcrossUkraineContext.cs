using Microsoft.EntityFrameworkCore;
using travelAcrossUkraine.WebApi.Entities;

namespace travelAcrossUkraine.WebApi.Context;

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
            entity.Property(p => p.CoordinateY)
                        .HasPrecision(20, 10);
        });

        //modelBuilder.Entity<Course>(entity =>
        //{
        //    entity.Property(e => e.CourseName)
        //        .HasMaxLength(50)
        //        .IsUnicode(false);

        //    entity.HasOne(d => d.Teacher)
        //        .WithMany(p => p.Course)
        //        .HasForeignKey(d => d.TeacherId)
        //        .OnDelete(DeleteBehavior.Cascade)
        //        .HasConstraintName("FK_Course_Teacher");
        //});
    }
    public DbSet<GeoPointEntity> GeoPoints { get; set; }
}
