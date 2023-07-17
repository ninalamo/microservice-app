using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure;
using Azure.Messaging.EventGrid;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using sample.domain.AggregateModels.CustomerAggregate;
using sample.infrastructure.EventGrid;

namespace sample.az_functions;

public class CustomerDbChanged
{
    private IEventService _eventService;

    public CustomerDbChanged(IEventService eventService)
    {
        _eventService = eventService;
    }
    
    [FunctionName("CustomerDbChanged")]
    public async Task RunAsync([CosmosDBTrigger(
            databaseName: "customer-db",
            collectionName: "customer",
            ConnectionStringSetting = "DefaultConnection",
            LeaseCollectionName = "customer-lease",
            CreateLeaseCollectionIfNotExists = true
            )]
        IReadOnlyList< Microsoft.Azure.Documents.Document> input, ILogger log)
    {
        log.LogInformation("{Input}", input);
        if (input != null && input.Count > 0)
        {
            log.LogInformation("Documents modified " + input.Count);
            log.LogInformation("First document Id " + input[0].Id);
            await _eventService.SendAsync(new List<EventGridEvent>
            {
                // EventGridEvent with custom model serialized to JSON
                new EventGridEvent(
                    "customer-cosmosdb-change",
                    "Example.EventType",
                    "1.0",
                    input),
            });
        }


    }
}