using System.Linq;

namespace Valiasr.DataAccess.Repositories
{
    using System;

    using Valiasr.Domain.Model;
    using Valiasr.Domain.Repositories;

    public class LawyerRepository:Repository<Lawyer>,ILawyerRepository
    {
        public void AddLawyer(string melliIdentity, DateTime startDate)
        {
            Person person = (from p in ActiveContext.Persons where p.NationaliIdentity == melliIdentity select p).FirstOrDefault();
            Lawyer lawyer = Lawyer.CreateLawyer(person);
            Add(lawyer);

        }

    }
}
