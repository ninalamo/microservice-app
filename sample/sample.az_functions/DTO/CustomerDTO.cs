using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample.az_functions.DTO
{
    public class CustomerDTO
    {
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public DateTime Birthday { get;  set; }
        public string Email { get;  set; }
    }
}
