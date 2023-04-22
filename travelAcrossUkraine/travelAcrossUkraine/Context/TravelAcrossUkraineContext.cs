﻿using Microsoft.EntityFrameworkCore;
using TravelAcrossUkraine.WebApi.Entities;
using TravelAcrossUkraine.WebApi.Entities.BaseEntities;

namespace TravelAcrossUkraine.WebApi.Context;

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
            entity.HasQueryFilter(ent => !ent.IsDeleted);
        });

        modelBuilder.Entity<PolygonEntity>().HasQueryFilter(ent => !ent.IsDeleted);

        modelBuilder.Entity<CircleEntity>(entity =>
        {
            entity.Property(p => p.Radius).HasPrecision(20, 10);
            entity.HasQueryFilter(ent => !ent.IsDeleted);
        });

        modelBuilder.Entity<TypeEntity>().HasQueryFilter(ent => !ent.IsDeleted);
        modelBuilder.Entity<CategoryEntity>().HasQueryFilter(ent => !ent.IsDeleted);
        modelBuilder.Entity<ImageEntity>().HasQueryFilter(ent => !ent.IsDeleted);
        modelBuilder.Entity<LocationEntity>().HasQueryFilter(ent => !ent.IsDeleted);
    }

    public DbSet<GeoPointEntity> GeoPoints { get; set; }
    public DbSet<PolygonEntity> Polygons { get; set; }
    public DbSet<CircleEntity> Circles { get; set; }
    public DbSet<TypeEntity> Types { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<ImageEntity> Images { get; set; }
    public DbSet<LocationEntity> Locations { get; set; }



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



//[
//    {
//        "id": "1675192786417",
//        "type": "Point",
//        "geometries": [
//            2790819.1192107527,
//            6994940.20348488
//        ]
//    },
//    {
//    "id": "1675192959972",
//        "type": "Circle",
//        "geometries": {
//        "center": [
//                2081483.496724317,
//                6833505.199746588
//            ],
//            "radius": 243028.01847738423
//        }
//},
//    {
//    "id": "1675192978122",
//        "type": "LineString",
//        "geometries": [
//            [
//                2130403.1948268297,
//                6085033.818778142
//            ],
//            [
//                2673411.843764722,
//                6310064.430049701
//            ],
//            [
//                2673411.843764722,
//                6094817.758398645
//            ],
//            [
//                2918010.334277286,
//                6261144.731947188
//            ]
//        ]
//    },
//    {
//    "id": "1675192987434",
//        "type": "Polygon",
//        "geometries": [
//            [
//                [
//                    2570680.477749445,
//                    7577084.610904782
//                ],
//                [
//                    2976713.9720003013,
//                    7635788.248627798
//                ],
//                [
//                    2976713.9720003013,
//                    7435217.486407495
//                ],
//                [
//                    2683195.7833852246,
//                    7396081.727925485
//                ],
//                [
//                    2570680.477749445,
//                    7577084.610904782
//                ]
//            ]
//        ]
//    }
//]
