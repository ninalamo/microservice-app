using Microsoft.Extensions.DependencyInjection;
using sample.domain.AggregateModels.CustomerAggregate;
using sample.domain.Seedwork;
using sample.infrastructure.AppSettings;
using sample.infrastructure.CosmosDbData;
using sample.infrastructure.CosmosDbData.Interfaces;
using sample.infrastructure.CosmosDbData.Repository;

namespace sample.infrastructure;

public static class RegisterService
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string endpoint,
        string primaryKey, string databaseName, List<ContainerInfo> containers)
    {
        var client = new CosmosClient(endpoint, primaryKey, new CosmosClientOptions() { ApplicationName = "CosmosDbApp" });
        var cosmosDbClientFactory = new CosmosDbContainerFactory(client, databaseName, containers);
        cosmosDbClientFactory.EnsureDbSetupAsync();

        services.AddSingleton<ICosmosDbContainerFactory>(cosmosDbClientFactory);

        foreach (var info in containers)
        {
            services.AddScoped<ICustomerRepository>(c => new CustomerRepository(cosmosDbClientFactory, info.Name));
        }
        return services;
    }
}

