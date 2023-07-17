using sample.domain.AggregateModels.CustomerAggregate;
using sample.domain.Seedwork;

namespace sample.domain.AggregateModels.CustomerAggregate
{
    public class Customer : Entity
    {
        public Customer(string firstname, string lastname, double birthdayInEpoch, string email) { 
            FirstName = firstname;
            LastName = lastname;
            BirthdayInEpoch = birthdayInEpoch;
            Email = email;
            Id = Guid.NewGuid().ToString();
            PartitionKey = Id;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set;}
        public double BirthdayInEpoch { get; private set; }
        public string Email { get; private set; }
    }
}

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer> CreateAsync(Customer customer);
    Task<Customer> UpdateAsync(string id, Customer customer);
}
