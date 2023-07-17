using Microsoft.Extensions.DependencyInjection;
using sample.domain.AggregateModels.CustomerAggregate;
using sample.domain.Seedwork;
using sample.infrastructure.AppSettings;
using sample.infrastructure.CosmosDbData;
using sample.infrastructure.CosmosDbData.Interfaces;

namespace sample.infrastructure;

public static class RegisterService
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string endpoint,
        string primaryKey, string databaseName, List<ContainerInfo> containers)
    {
        var client = new CosmosClient(endpoint, primaryKey);
        var cosmosDbClientFactory = new CosmosDbContainerFactory(client, databaseName, containers);

        services.AddSingleton<ICosmosDbContainerFactory>(cosmosDbClientFactory);


        return services;
    }
}

