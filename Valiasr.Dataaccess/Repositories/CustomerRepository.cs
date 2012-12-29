namespace Valiasr.DataAccess.Repositories
{
    using System.Linq;

    using Valiasr.Domain.Model;
    using Valiasr.Domain.Repositories;

    public class CustomerRepository:Repository<Customer>,ICustomerRepository
    {
        public void AddCustomer(string melliIdentity, string no, float portion)
        {
            Person person = (from p in ActiveContext.Persons where p.NationaliIdentity == melliIdentity select p).FirstOrDefault();
            Customer customer = Customer.CreateCustomer(person, no, portion);
            Add(customer);            
        }
    }
}
