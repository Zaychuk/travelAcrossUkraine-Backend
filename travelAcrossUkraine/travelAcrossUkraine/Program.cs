
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TravelAcrossUkraine.WebApi.Context;
using TravelAcrossUkraine.WebApi.Mappers;
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
services.AddSingleton(serviceColl => new AutoMapperConfigure().GetMapper());

void ConfigurePolicy(CorsPolicyBuilder builder) =>

    builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();


services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", ConfigurePolicy);
});


// Sevices
services.AddScoped<IGeoPointService, GeoPointService>();
services.AddScoped<IPolygonService, PolygonService>();
services.AddScoped<ICircleService, CircleService>();
services.AddScoped<ITypeService, TypeService>();
services.AddScoped<ICategoryService, CategoryService>();
services.AddScoped<ILocationService, LocationService>();
services.AddScoped<IAuthService, AuthService>();
services.AddScoped<IUserService, UserService>();
services.AddScoped<ICollectionService, CollectionService>();

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
services.AddScoped<IImageRepository, ImageRepository>();


services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
