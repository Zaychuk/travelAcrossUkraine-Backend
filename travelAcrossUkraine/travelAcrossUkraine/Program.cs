using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TravelAcrossUkraine.WebApi.Context;
using TravelAcrossUkraine.WebApi.Mappings;
using TravelAcrossUkraine.WebApi.Repositories;
using TravelAcrossUkraine.WebApi.Services;
using TravelAcrossUkraine.WebApi.Services.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

services.AddDbContext<TravelAcrossUkraineContext>(o => o.UseSqlServer(builder.Configuration["DataBaseConnection:ConnectionString"]));

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
services.AddScoped<IGeoPointService, GeoPointService>();
services.AddScoped<IPolygonService, PolygonService>();
services.AddScoped<ICircleService, CircleService>();
services.AddScoped<ITypeService, TypeService>();
services.AddScoped<ICategoryService, CategoryService>();
services.AddScoped<ILocationService, LocationService>();
services.AddScoped<IAuthService, AuthService>();

// Repositories
services.AddScoped<IGeoPointRepository, GeoPointRepository>();
services.AddScoped<IPolygonRepository, PolygonRepository>();
services.AddScoped<ICircleRepository, CircleRepository>();
services.AddScoped<ITypeRepository, TypeRepository>();
services.AddScoped<ICategoryRepository, CategoryRepository>();
services.AddScoped<ILocationRepository, LocationRepository>();
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IRoleRepository, RoleRepository>();
services.AddScoped<ICollectionRepository, CollectionRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
