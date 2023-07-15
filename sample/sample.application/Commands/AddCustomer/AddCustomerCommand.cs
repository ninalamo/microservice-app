using System.Runtime.Serialization;
using MediatR;

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
    public AddCustomerCommandHandler()
    {
        
    }
    public async Task<Guid> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
    {
        return Guid.NewGuid();
    }
}