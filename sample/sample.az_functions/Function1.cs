using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using sample.az_functions.DTO;

namespace sample.az_functions
{
    public class Function1
    {
        [FunctionName("customers")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            
            
            var customer = JsonConvert.DeserializeObject<CustomerDTO>(requestBody);
            var addCustomerCommand =
                new AddCustomerCommand(customer.FirstName, customer.LastName, customer.Birthday, customer.Email);

            var result = await _mediator.Send(addCustomerCommand);

            return OkResult(result);


        }
    }
}
