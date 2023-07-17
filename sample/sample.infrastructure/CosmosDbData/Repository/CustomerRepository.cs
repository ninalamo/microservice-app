using sample.domain.AggregateModels.CustomerAggregate;
using sample.domain.Seedwork;
using sample.infrastructure.CosmosDbData.Interfaces;

namespace sample.infrastructure.CosmosDbData.Repository;

public static class StringExtensions
{
    public static PartitionKey ToPartitionKey(this string str) => new PartitionKey(str);
}
public class CustomerRepository : ICustomerRepository
{
    private readonly ICosmosDbContainerFactory _cosmosDbContainerFactory;
    private readonly Container _container;
    
    public CustomerRepository(ICosmosDbContainerFactory cosmosDbContainerFactory, string? containerName) 
    {
        this._cosmosDbContainerFactory = cosmosDbContainerFactory ?? throw new ArgumentNullException(nameof(cosmosDbContainerFactory));
        this._container = _cosmosDbContainerFactory.GetContainer(containerName)._container;
    }


    public async Task<Customer> CreateAsync(Customer customer)
    {
        return await _container.CreateItemAsync(customer,customer.PartitionKey?.ToPartitionKey());
    }

    public async Task<Customer> UpdateAsync(string id, Customer customer)
    {
        return await this._container.UpsertItemAsync(customer, id.ToPartitionKey());
    }
}