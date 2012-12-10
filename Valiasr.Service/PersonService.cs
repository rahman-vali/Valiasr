using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Valiasr.DataAccess;
using Valiasr.Domain;

namespace Valiasr.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
     
    public class PersonService : IPersonService
     {
        public PersonDto GetPerson(string name)
        {
            ValiasrContext context = new ValiasrContext("Valiasr");
            Customer customer = context.Customers.First(p => p.Firstname == name);
            return new PersonDto()

                {
                    Firstname = customer.Firstname,
                    Lastname = customer.Lastname,
                };
        }
     }
   
}
