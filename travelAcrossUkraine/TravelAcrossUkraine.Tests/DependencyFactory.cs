using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using TravelAcrossUkraine.Tests.Mocks;
using TravelAcrossUkraine.WebApi.Context;
using TravelAcrossUkraine.WebApi.Controllers;
using TravelAcrossUkraine.WebApi.Mappers;
using TravelAcrossUkraine.WebApi.Repositories;
using TravelAcrossUkraine.WebApi.Services;

namespace TravelAcrossUkraine.Tests;

internal class DependencyFactory
{

    #region Controllers
    public static CategoriesController GetCategoriesController(TravelAcrossUkraineContextMock context)
    {
        return new CategoriesController(GetCategoryService(context), NullLogger<CategoriesController>.Instance);
    }
    public static CollectionsController GetCollectionsController(TravelAcrossUkraineContextMock context)
    {
        return new CollectionsController(GetCollectionService(context), NullLogger<CollectionsController>.Instance);
    }
    public static TypesController GetTypesController(TravelAcrossUkraineContextMock context)
    {
        return new TypesController(GetTypeService(context), NullLogger<TypesController>.Instance);
    }

    #endregion

    #region Services
    public static ICategoryService GetCategoryService(TravelAcrossUkraineContextMock context)
    {
        return new CategoryService(GetCategoryRepository(context), GetAutomapper());
    }
    public static ICircleService GetCircleService(TravelAcrossUkraineContextMock context)
    {
        return new CircleService(GetCircleRepository(context), GetAutomapper());
    }
    public static ICollectionService GetCollectionService(TravelAcrossUkraineContextMock context)
    {
        return new CollectionService(GetCollectionRepository(context), GetUserRepository(context), new HttpContextAccessor(), GetAutomapper());
    }
    public static IGeoPointService GetGeoPointService(TravelAcrossUkraineContextMock context)
    {
        return new GeoPointService(GetGeoPointRepository(context), GetAutomapper());
    }
    public static ILocationService GetLocationService(TravelAcrossUkraineContextMock context)
    {
        return new LocationService(GetLocationRepository(context), GetImageRepository(context), new HttpContextAccessor(), GetAutomapper());
    }
    public static IPolygonService GetPolygonService(TravelAcrossUkraineContextMock context)
    {
        return new PolygonService(GetPolygonRepository(context), GetAutomapper());
    }
    public static ITypeService GetTypeService(TravelAcrossUkraineContextMock context)
    {
        return new TypeService(GetTypeRepository(context), GetAutomapper());
    }
    #endregion

    #region Repositories
    public static ILocationRepository GetLocationRepository(TravelAcrossUkraineContextMock context)
    {
        return new LocationRepository(context);
    }
    public static ICategoryRepository GetCategoryRepository(TravelAcrossUkraineContextMock context)
    {
        return new CategoryRepository(context);
    }
    public static ICircleRepository GetCircleRepository(TravelAcrossUkraineContextMock context)
    {
        return new CircleRepository(context);
    }
    public static ICollectionRepository GetCollectionRepository(TravelAcrossUkraineContextMock context)
    {
        return new CollectionRepository(context);
    }
    public static IGeoPointRepository GetGeoPointRepository(TravelAcrossUkraineContextMock context)
    {
        return new GeoPointRepository(context);
    }
    public static IImageRepository GetImageRepository(TravelAcrossUkraineContextMock context)
    {
        return new ImageRepository(context);
    }
    public static IPolygonRepository GetPolygonRepository(TravelAcrossUkraineContextMock context)
    {
        return new PolygonRepository(context);
    }
    public static IRoleRepository GetRoleRepository(TravelAcrossUkraineContextMock context)
    {
        return new RoleRepository(context);
    }
    public static ITypeRepository GetTypeRepository(TravelAcrossUkraineContextMock context)
    {
        return new TypeRepository(context);
    }
    public static IUserRepository GetUserRepository(TravelAcrossUkraineContextMock context)
    {
        return new UserRepository(context);
    }
    #endregion

    #region Automapper
    public static IMapper GetAutomapper()
    {
        return new AutoMapperConfigure().GetMapper();
    }
    #endregion

    #region Context

    public static TravelAcrossUkraineContextMock GetContext<T>()
    {
        var fileName = GetContextFilename<T>();
        return GetPortalContextWithSqlLiteDatabaseMock(fileName);
    }

    private static string GetContextFilename<T>()
    {
        Type myType = typeof(T);
        var name = myType.Name;
        var nsp = myType.Namespace;
        return $"{nsp}.{name}.{DateTime.Now.Ticks}";
    }

    private static TravelAcrossUkraineContextMock GetPortalContextWithSqlLiteDatabaseMock(string fileName)
    {
        var options = new DbContextOptionsBuilder<TravelAcrossUkraineContext>()
            // Logs error to console
            .EnableSensitiveDataLogging()
            .UseSqlite($"DataSource={fileName}.db").Options;
        var context = new TravelAcrossUkraineContextMock(options);
        return context;
    }
    #endregion
}
