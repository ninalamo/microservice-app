using System.IO;
using Azure.Messaging.EventGrid;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sample.application;
using sample.az_functions;
using sample.domain.AggregateModels.CustomerAggregate;
using sample.infrastructure;
using sample.infrastructure.AppSettings;
using sample.infrastructure.CosmosDbData.Repository;
using sample.infrastructure.EventGrid;

[assembly: FunctionsStartup(typeof(Startup))]

namespace sample.az_functions;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

         builder.Services.AddSingleton<IConfiguration>(configuration);
        
        CosmosDbSettings cosmosDbConfig = configuration
            .GetSection("ConnectionStrings:CosmosDbSettings")
            .Get<CosmosDbSettings>();
        
        builder.Services.AddInfrastructure(cosmosDbConfig.EndpointUrl,
            cosmosDbConfig.PrimaryKey,
            cosmosDbConfig.DatabaseName,
            cosmosDbConfig.Containers);
        
        EventGridSettings eventConfig = configuration
            .GetSection("ConnectionStrings:EventGridSettings")
            .Get<EventGridSettings>();

        builder.Services.AddScoped<IEventService>(e =>
            new EventService(eventConfig.TopicEndpoint, eventConfig.TopicAccessKey));



         builder.Services.AddApplication();
    }
}