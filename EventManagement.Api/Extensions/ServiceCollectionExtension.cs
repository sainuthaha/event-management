using Microsoft.EntityFrameworkCore;
using EventManagement.Api.Data;
using Microsoft.Data.SqlClient;
using Azure.Identity;

namespace EventManagement.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
       public static void AddAzureSqlDbContext(this IServiceCollection services, IConfiguration configuration)
        {
          // Get the connection string from appsettings.json
            var connectionString = configuration.GetConnectionString("AzureSqlDb");

            // Use DefaultAzureCredential for authentication (supports Managed Identity, Azure CLI, etc.)
            var credential = new DefaultAzureCredential();

            // Create the SQL connection and get the Azure token
            var sqlConnection = new SqlConnection(connectionString);
         
            // Configure EF Core to use the SQL connection
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(sqlConnection));
        }
    }
}