using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace sample.az_functions;

public static class CustomerDbChanged
{
    [FunctionName("CustomerDbChanged")]
    public static async Task RunAsync([CosmosDBTrigger(
            databaseName: "customer-db",
            collectionName: "customer",
            ConnectionStringSetting = "DefaultConnection",
            LeaseCollectionName = "customer-lease",
            CreateLeaseCollectionIfNotExists = true
            )]
        IReadOnlyList<Document> input, ILogger log)
    {
        log.LogInformation("{Input}", input);
        if (input != null && input.Count > 0)
        {
            log.LogInformation("Documents modified " + input.Count);
            log.LogInformation("First document Id " + input[0].Id);
            
        }
    }
}