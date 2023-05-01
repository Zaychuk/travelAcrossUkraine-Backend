using Microsoft.EntityFrameworkCore;
using TravelAcrossUkraine.WebApi.Entities;
using TravelAcrossUkraine.WebApi.Entities.BaseEntities;

namespace TravelAcrossUkraine.WebApi.Context;

public class TravelAcrossUkraineContext : DbContext
{
    public TravelAcrossUkraineContext(DbContextOptions<TravelAcrossUkraineContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryEntity>().HasQueryFilter(ent => !ent.IsDeleted);
        modelBuilder.Entity<CircleEntity>(entity =>
        {
            entity.Property(p => p.Radius).HasPrecision(20, 10);
            entity.HasQueryFilter(ent => !ent.IsDeleted);
        });
        modelBuilder.Entity<CollectionEntity>().HasQueryFilter(ent => !ent.IsDeleted);
        modelBuilder.Entity<CollectionLocationEntity>().HasQueryFilter(ent => !ent.IsDeleted);
        modelBuilder.Entity<GeoPointEntity>(entity =>
        {
            entity.Property(p => p.CoordinateX).HasPrecision(20, 10);
            entity.Property(p => p.CoordinateY).HasPrecision(20, 10);
            entity.HasQueryFilter(ent => !ent.IsDeleted);
        });
        modelBuilder.Entity<ImageEntity>().HasQueryFilter(ent => !ent.IsDeleted);
        modelBuilder.Entity<LocationEntity>().HasQueryFilter(ent => !ent.IsDeleted);
        modelBuilder.Entity<PolygonEntity>().HasQueryFilter(ent => !ent.IsDeleted);
        modelBuilder.Entity<RoleEntity>().HasQueryFilter(ent => !ent.IsDeleted);
        modelBuilder.Entity<TypeEntity>().HasQueryFilter(ent => !ent.IsDeleted);
        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasQueryFilter(ent => !ent.IsDeleted);
            entity.HasIndex(u => u.EmailAddress).IsUnique();
            entity.HasIndex(u => u.Username).IsUnique();
        });
    }

    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<CircleEntity> Circles { get; set; }
    public DbSet<CollectionEntity> Collections { get; set; }
    public DbSet<CollectionLocationEntity> CollectionLocation { get; set; }
    public DbSet<GeoPointEntity> GeoPoints { get; set; }
    public DbSet<ImageEntity> Images { get; set; }
    public DbSet<LocationEntity> Locations { get; set; }
    public DbSet<PolygonEntity> Polygons { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<TypeEntity> Types { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    public async new Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        UpdateSoftDeleteEntityState();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateSoftDeleteEntityState()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            var isSoftDeleteEntity = entry.Entity is SoftDeleteBaseEntity;
            if (!isSoftDeleteEntity)
            {
                continue;
            }
            var entity = entry.Entity as SoftDeleteBaseEntity;
            switch (entry.State)
            {
                case EntityState.Added:
                    entity.IsDeleted = false;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entity.IsDeleted = true;
                    break;
            }
        }
    }
}
