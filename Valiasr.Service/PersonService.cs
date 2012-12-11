using System.Linq;
using Valiasr.DataAccess;

namespace Valiasr.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
     
    public class PersonService : IPersonService
     {
        public PersonDto GetPerson(string name)
        {
            var context = new ValiasrContext("Valiasr.Ce");
            var customer = context.Customers.First(p => p.Firstname == name);
            return new PersonDto()

                {
                    Firstname = customer.Firstname,
                    Lastname = customer.Lastname,
                };
        }
     }
   
}
