using AutoMapper;
using TravelAcrossUkraine.WebApi.Mappings;
using TravelAcrossUkraine.WebApi.Repositories;
using TravelAcrossUkraine.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// AutoMapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader())
);

IMapper mapper = mapperConfig.CreateMapper();
services.AddSingleton(mapper);

// Sevices
services.AddSingleton<IGeoPointService, GeoPointService>();
services.AddSingleton<IPolygonService, PolygonService>();
services.AddSingleton<ICircleService, CircleService>();
services.AddSingleton<ITypeService, TypeService>();
services.AddSingleton<ICategoryService, CategoryService>();
services.AddSingleton<ILocationService, LocationService>();

// Repositories
services.AddSingleton<IGeoPointRepository, GeoPointRepository>();
services.AddSingleton<IPolygonRepository, PolygonRepository>();
services.AddSingleton<ICircleRepository, CircleRepository>();
services.AddSingleton<ITypeRepository, TypeRepository>();
services.AddSingleton<ICategoryRepository, CategoryRepository>();
services.AddSingleton<ILocationRepository, LocationRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
