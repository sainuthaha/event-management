using Microsoft.EntityFrameworkCore;
using EventManagement.Api.Data;
using Azure.Identity;

namespace EventManagement.Api.Extensions
{
  public static class ServiceCollectionExtension
  {
    public static void AddAzureSqlDbContext(this IServiceCollection services, IConfiguration configuration)
    {
      var connectionString = configuration.GetConnectionString("AzureSqlDb");

      services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString, sqlServerOptions =>
          sqlServerOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null)));
    }
  }
}
