using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using TodoApp.Application.Helpers.Jwt;
using TodoApp.Application.Helpers.Mapping;
using TodoApp.Application.Helpers.Security;
using TodoApp.Application.Services;
using TodoApp.Domain.Interfaces.Helpers;
using TodoApp.Domain.Interfaces.Repositories;
using TodoApp.Domain.Interfaces.Services;
using TodoApp.Persistence;
using TodoApp.Persistence.Repositories;

namespace TodoApp.API.Helpers.Extensions;

public static class AppExtension
{
   public static void AddDbConnection(this IServiceCollection serviceCollection, IConfiguration configuration)
   {
      serviceCollection.AddDbContext<AppDbContext>(options =>
      {
         options.UseNpgsql(configuration.GetConnectionString(nameof(AppDbContext)));
      });
   }
   
   public static void AddRepositories(this IServiceCollection serviceCollection)
   {
      serviceCollection.AddScoped<IUserRepository, UserRepository>();
      serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();
      serviceCollection.AddScoped<ITaskRepository, TaskRepository>();
      serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
   }
   
   public static void AddServices(this IServiceCollection serviceCollection)
   {
      serviceCollection.AddScoped<IAuthService, AuthService>();
      serviceCollection.AddScoped<ITokenService, TokenService>();
      serviceCollection.AddScoped<ICategoryService, CategoryService>();
   }

   public static void AddHelpers(this IServiceCollection serviceCollection, IConfiguration configuration)
   {
      serviceCollection.AddScoped<IPasswordHasher, PasswordHasher>();
      serviceCollection.AddScoped<IJwtProvider, JwtProvider>();
      serviceCollection.AddAutoMapper(cfg => { cfg.LicenseKey = configuration["AutoMapper:LicenseKey"]; }, typeof(MappingProfiles));
   }
   
   public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
   {
      services.AddSwaggerGen(options =>
      {
         options.SwaggerDoc("v1", new OpenApiInfo
         {
            Title = "Store.API",
            Version = "v1"
         });
         
         options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
         {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter JWT",
         });
         
         options.EnableAnnotations();
         
         options.AddSecurityRequirement(new OpenApiSecurityRequirement
         {
            {
               new OpenApiSecurityScheme
               {
                  Reference = new OpenApiReference
                  {
                     Type = ReferenceType.SecurityScheme,
                     Id = "Bearer"
                  }
               },
               Array.Empty<string>()
            }
         });
      });

      return services;
   }
   
}