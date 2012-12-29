namespace Valiasr.DataAccess.Repositories
{
    using System.Linq;

    using Valiasr.Domain.Model;
    using Valiasr.Domain.Repositories;

    public class PersonRepository:Repository<Person>,IPersonRepository
    {
        

        public Person GetPerson(string nationalIdentity)
        {
            return
                (this.ActiveContext.Persons.Where(
                    p => nationalIdentity != null && p.NationaliIdentity == nationalIdentity)).FirstOrDefault();
        }

      


       
    }
}