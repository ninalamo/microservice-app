using System.IO;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sample.application;
using sample.az_functions;
using sample.infrastructure;
using sample.infrastructure.AppSettings;
using sample.infrastructure.CosmosDbData.Repository;

[assembly: FunctionsStartup(typeof(Startup))]

namespace sample.az_functions;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        // IConfigurationRoot configuration = new ConfigurationBuilder()
        //     .SetBasePath(Directory.GetCurrentDirectory())
        //     .AddJsonFile($"local.settings.json", optional: true, reloadOnChange: true)
        //     .AddEnvironmentVariables()
        //     .Build();
        //
        // builder.Services.AddSingleton<IConfiguration>(configuration);
        //
        // CosmosDbSettings cosmosDbConfig = configuration
        //     .GetSection("ConnectionStrings:CosmosDbSettings")
        //     .Get<CosmosDbSettings>();
        //
        // builder.Services.AddInfrastructure(cosmosDbConfig.EndpointUrl,
        //     cosmosDbConfig.PrimaryKey,
        //     cosmosDbConfig.DatabaseName,
        //     cosmosDbConfig.Containers);
        //
        //  builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        //
         builder.Services.AddApplication();
    }
}