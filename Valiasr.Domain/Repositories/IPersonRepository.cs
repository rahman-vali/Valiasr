namespace Valiasr.Domain.Repositories
{
    using Valiasr.Domain.Model;

    public interface IPersonRepository:IRepository<Person>
    {
        Person GetPerson(string nationalIdentity);
    }
}
