namespace Valiasr.Domain.Repositories
{
    using Valiasr.Domain.Model;

    public interface ICustomerRepository:IRepository<Customer>
    {
        void AddCustomer(string melliIdentity, string no, float portion);
    }
}
