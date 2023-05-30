using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAcrossUkraine.WebApi.Context;

namespace TravelAcrossUkraine.Tests.Mocks;

public class TravelAcrossUkraineContextMock : TravelAcrossUkraineContext
{
    public TravelAcrossUkraineContextMock(DbContextOptions<TravelAcrossUkraineContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
        {
            // Hack from https://blog.dangl.me/archive/handling-datetimeoffset-in-sqlite-with-entity-framework-core/ To deal with DateTimeOffset with SQLite

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(DateTimeOffset)
                                                                            || p.PropertyType == typeof(DateTimeOffset?));
                foreach (var property in properties)
                {
                    builder
                        .Entity(entityType.Name)
                        .Property(property.Name)
                        .HasConversion(new DateTimeOffsetToBinaryConverter());
                }
            }
        }
    }
}
