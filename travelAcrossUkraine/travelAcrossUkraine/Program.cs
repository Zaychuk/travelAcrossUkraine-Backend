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

IMapper mapper = mapperConfig.CreateMapper();
services.AddSingleton(mapper);

// Sevices
services.AddSingleton<IGeoPointService, GeoPointService>();
services.AddSingleton<IPolygonService, PolygonService>();
services.AddSingleton<ICircleService, CircleService>();

// Repositories
services.AddSingleton<IGeoPointRepository, GeoPointRepository>();
services.AddSingleton<IPolygonRepository, PolygonRepository>();
services.AddSingleton<ICircleRepository, CircleRepository>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
