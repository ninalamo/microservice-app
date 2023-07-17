using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using sample.domain.AggregateModels.CustomerAggregate;

namespace sample.application.Commands.AddCustomer;

public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, Guid>
{
    private readonly ILogger<AddCustomerCommandHandler> _logger;
    private readonly ICustomerRepository _repository;
    public AddCustomerCommandHandler(ICustomerRepository repository, ILogger<AddCustomerCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    public async Task<Guid> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _repository.CreateAsync(
            new Customer(
                request.FirstName, 
                request.LastName,
                request.Birthday.ToEpoch(),
            request.Email));
        
        return Guid.Parse(customer.Id ?? string.Empty);
    }


}