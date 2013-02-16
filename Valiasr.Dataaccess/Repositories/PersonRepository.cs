namespace Valiasr.DataAccess.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Valiasr.Domain.Model;
    using Valiasr.Domain.Repositories;

    public class PersonRepository:Repository<Person>,IPersonRepository
    {

        public Person GetPersonByNationalIdentity(string nationalIdentity)
        {
            return ActiveContext.Persons.Where(p => p.NationalIdentity == nationalIdentity).FirstOrDefault();
        }

        public bool PersonIsCustomerOrLawyer(Guid id, ref string messageStr)
        {
            var customerAccounts =
                ActiveContext.BankAccounts.OfType<Account>().Include("Customers.Person")
                          .Where(a => a.Customers.Any(c => c.Person.Id == id))
                          .ToList();
            if (customerAccounts.Any())
            {
                messageStr = "this person is customer in these/this accounts :  " +
                             string.Join(",", customerAccounts.Select(ca => ca.Code).ToArray());
            }
            var lawyerAccounts =
                ActiveContext.BankAccounts.OfType<Account>().Include("Lawyers.Person")
                          .Where(a => a.Customers.Any(c => c.Person.Id == id))
                          .ToList();
            if (lawyerAccounts.Any())
            {
                messageStr = string.Format("{0} And lawyer in these/this accounts:  {1}", messageStr, string.Join(",", lawyerAccounts.Select(la => la.Code).ToArray()));
            }
            return messageStr.Length != 0;
        }

        public bool PersonIsCustomerOrLawyer(string natinalIdentity, ref string messageStr)
        {
            var customerAccounts =
                ActiveContext.BankAccounts.OfType<Account>().Include("Customers.Person")
                          .Where(a => a.Customers.Any(c => c.Person.NationalIdentity == natinalIdentity))
                          .ToList();
            if (customerAccounts.Any())
            {
                messageStr = "this person is customer in these/this accounts :  " +
                             string.Join(",", customerAccounts.Select(ca => ca.Code).ToArray());
            }
            var lawyerAccounts =
                ActiveContext.BankAccounts.OfType<Account>().Include("Lawyers.Person")
                          .Where(a => a.Customers.Any(c => c.Person.NationalIdentity == natinalIdentity))
                          .ToList();
            if (lawyerAccounts.Any())
            {
                messageStr = string.Format("{0} And lawyer in these/this accounts:  {1}", messageStr, string.Join(",", lawyerAccounts.Select(la => la.Code).ToArray()));
            }
            return messageStr.Length != 0;
        }

    }
}