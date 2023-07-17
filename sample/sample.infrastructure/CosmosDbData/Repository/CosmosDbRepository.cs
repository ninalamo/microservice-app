using Microsoft.Azure.Cosmos.Linq;
using sample.domain.AggregateModels.CustomerAggregate;
using sample.domain.Seedwork;
using sample.infrastructure.CosmosDbData.Interfaces;

namespace sample.infrastructure.CosmosDbData.Repository;
public abstract class CosmosDbRepository<T> : IRepository<T>, IContainerContext<T> where T : Entity
{
        public virtual string ContainerName { get; }
        
        /// <summary>
        ///     Cosmos DB factory
        /// </summary>
        private readonly ICosmosDbContainerFactory _cosmosDbContainerFactory;
        
        /// <summary>
        ///     Cosmos DB container
        /// </summary>
        private readonly Container _container;
        
        public CosmosDbRepository(ICosmosDbContainerFactory cosmosDbContainerFactory)
        {
            this._cosmosDbContainerFactory = cosmosDbContainerFactory ?? throw new ArgumentNullException(nameof(ICosmosDbContainerFactory));
            this._container = this._cosmosDbContainerFactory.GetContainer(ContainerName)._container;
        }

        public async Task AddItemAsync(T item)
        {
            item.Id = GenerateId(item);
            await _container.CreateItemAsync<T>(item, ResolvePartitionKey(item.Id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<T>(id, ResolvePartitionKey(id));
        }

        public async Task<T> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<T> response = await _container.ReadItemAsync<T>(id, ResolvePartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task UpdateItemAsync(string id, T item)
        {
            // Audit
            //await Audit(item);
            // Update
            await this._container.UpsertItemAsync<T>(item, ResolvePartitionKey(id));
        }
        
        // code not relevant to this article is skipped
        public string GenerateId(T entity)
        {
            throw new NotImplementedException();
        }

        public PartitionKey ResolvePartitionKey(string entityId)
        {
            throw new NotImplementedException();
        }
    
}