namespace Valiasr.Domain.Repositories
{
    using System;

    using Valiasr.Domain.Model;

    public interface IPersonRepository:IRepository<Person>
    {
        Person GetPersonByNationalIdentity(string nationalIdentity);

        bool PersonIsCustomerOrLawyer(Guid id, ref string messageStr);

        bool PersonIsCustomerOrLawyer(string nationalIdentity, ref string messageStr );
    }
}
