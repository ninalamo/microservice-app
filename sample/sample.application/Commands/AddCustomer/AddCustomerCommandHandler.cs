using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using sample.domain.AggregateModels.CustomerAggregate;

namespace sample.application.Commands.AddCustomer;

public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, Guid>
{
    private readonly ILogger<AddCustomerCommandHandler> _logger;
    public AddCustomerCommandHandler(ILogger<AddCustomerCommandHandler> logger)
    {
        _logger = logger;
    }
    public async Task<Guid> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
    {
        var cosmosClient = new CosmosClient(
            "",
            "",
            new CosmosClientOptions() { ApplicationName = "CosmosDbApp" });

        var database = await cosmosClient.CreateDatabaseIfNotExistsAsync("customer-db");
        var container = await database.Database.CreateContainerIfNotExistsAsync("customer", "/partitionKey");
        
        var secondsSinceEpoch = request.Birthday.ToEpoch();

        var customer = new Customer(request.FirstName, request.LastName, secondsSinceEpoch, request.Email);
        
        var response = await container.Container.CreateItemAsync<Customer>(
            customer
            , new PartitionKey(customer.PartitionKey));

        return Guid.Parse(customer.Id);
    }


}