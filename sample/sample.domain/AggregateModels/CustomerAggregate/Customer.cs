using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sample.domain.AggregateModels.CustomerAggregate;

namespace sample.domain.AggregateModels.CustomerAggregate
{
    public class Customer : Entity, IAggregateRoot
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
    Task<Customer> UpdateAsync(Guid id, Customer customer);
}
