using AutoMapper;
using TravelAcrossUkraine.WebApi.MapperProfiles;

namespace TravelAcrossUkraine.WebApi.Mappers;

public class AutoMapperConfigure
{
    readonly MapperConfiguration _config;

    public AutoMapperConfigure()
    {
        _config = new MapperConfiguration(GetMapperConfiguration());
#if DEBUG
        // https://docs.automapper.org/en/stable/Custom-type-converters.html
        _config.AssertConfigurationIsValid(); // Throw error if smth configured wrong
#endif
    }

    public IMapper GetMapper()
    {
        return _config.CreateMapper();
    }

    private MapperConfigurationExpression GetMapperConfiguration()
    {
        var cfg = new MapperConfigurationExpression
        {
            AllowNullCollections = true
        };

        // Register profiles
        cfg.AddProfile(new CategoryProfile());
        cfg.AddProfile(new CircleProfile());
        cfg.AddProfile(new GeoPointProfile());
        cfg.AddProfile(new LocationProfile());
        cfg.AddProfile(new PolygonProfile());
        cfg.AddProfile(new TypeProfile());
        cfg.AddProfile(new UserProfile());

        return cfg;
    }
}
