using System;
using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using sample.application.Commands.AddCustomer;
using sample.az_functions.DTO;

namespace sample.az_functions
{
    public class CustomerApi
    {

        private readonly IMediator _mediator;

        public CustomerApi(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        
        [FunctionName("customers")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            
            
            var customer = JsonConvert.DeserializeObject<CustomerDTO>(requestBody);
            var addCustomerCommand =
                new AddCustomerCommand(customer.FirstName, customer.LastName, customer.Birthday, customer.Email);

            var result = await _mediator.Send(addCustomerCommand);

            return new OkObjectResult(result);


        }
    }
}
