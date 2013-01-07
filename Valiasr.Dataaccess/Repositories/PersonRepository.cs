namespace Valiasr.DataAccess.Repositories
{
    using System.Linq;

    using Valiasr.Domain.Model;
    using Valiasr.Domain.Repositories;

    public class PersonRepository:Repository<Person>,IPersonRepository
    {
        public Person GetPerson(string nationalIdentity)
        {
            return ActiveContext.Persons.Where(p => p.NationaliIdentity == nationalIdentity).FirstOrDefault();
        }
        public void CanBeDeleted()
        {
           
        }
    }
}