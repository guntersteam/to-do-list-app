using Microsoft.EntityFrameworkCore;
using TodoApp.Persistence;

namespace TodoApp.API.Helpers.Extensions;

public static class ApiExtension
{
   public static void AddDbConnection(this IServiceCollection serviceCollection, IConfiguration configuration)
   {
      serviceCollection.AddDbContext<AppDbContext>(options =>
      {
         options.UseNpgsql(configuration.GetConnectionString(nameof(AppDbContext)));
      });
   }
}