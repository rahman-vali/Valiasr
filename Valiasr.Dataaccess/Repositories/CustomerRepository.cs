namespace Valiasr.DataAccess.Repositories
{
    using System;
    using System.Linq;

    using Valiasr.Domain.Model;
    using Valiasr.Domain.Repositories;

    public class CustomerRepository:Repository<Customer>,ICustomerRepository
    {
       
    }
}
