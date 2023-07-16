using System.Runtime.Serialization;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using sample.domain.AggregateModels.CustomerAggregate;

namespace sample.application.Commands.AddCustomer;

public class AddCustomerCommand : IRequest<Guid>
{
    public AddCustomerCommand(string firstName, string lastName, DateTime birthday, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Birthday = birthday;
        Email = email;
    }
    
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime Birthday { get; private set; }
    public string Email { get; private set; }
}

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
            "xxx",
            "xxx",
            new CosmosClientOptions() { ApplicationName = "CosmosDbApp" });

        var database = await cosmosClient.CreateDatabaseIfNotExistsAsync("Customers");
        var container = await database.Database.CreateContainerIfNotExistsAsync("Customer", "/partitionKey");


        var customer = new Customer("nino", "alamo", 123141223, "nin.alamo@outlook.com");
        
        var response = await container.Container.CreateItemAsync<Customer>(
            customer
        , new PartitionKey(customer.PartitionKey));



        return Guid.Parse(customer.Id);
    }
}