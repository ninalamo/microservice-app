using sample.domain.AggregateModels.CustomerAggregate;
using sample.domain.Seedwork;
using sample.infrastructure.CosmosDbData.Interfaces;

namespace sample.infrastructure.CosmosDbData.Repository;

public class CustomerRepository : IRepository<Customer>
{
    public CustomerRepository(ICosmosDbContainerFactory cosmosDbContainerFactory, string containerName) : base(cosmosDbContainerFactory, containerName)
    {
    }

    public string GenerateId(Customer entity)
    {
       return entity.Id = Guid.NewGuid().ToString();
    }

    public PartitionKey ResolvePartitionKey(string entityId)
    {
        throw new NotImplementedException();
    }

    public IUnitOfWork UnitOfWork { get; }
}