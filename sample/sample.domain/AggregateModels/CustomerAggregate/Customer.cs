using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample.domain.AggregateModels.CustomerAggregate
{
    internal class Customer : Entity, IAggregateRoot
    {
        public Customer(string firstname, string lastname, double birthdayInEpoch, string email) { 
            FirstName = firstname;
            LastName = lastname;
            BirthdayInEpoch = birthdayInEpoch;
            Email = email;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set;}
        public double BirthdayInEpoch { get; private set; }
        public string Email { get; private set; }
    }
}
