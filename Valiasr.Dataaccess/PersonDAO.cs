namespace Valiasr.DataAccess
{
    using System;
    using System.Data;
    using System.Linq;

    using NUnit.Framework;

    using Valiasr.Domain;

    public class PersonDAO
    {
        public Person GetPerson(string melliIdentity)
        {
            var context = new ValiasrContext("Valiasr.ce");
            return (from p in context.Persons where p.MelliIdentity == melliIdentity select p).FirstOrDefault();
        }

        public Person GetAcount(string melliIdentity)
        {
            var context = new ValiasrContext("Valiasr.ce");
            return (from p in context.Persons where p.MelliIdentity == melliIdentity select p).FirstOrDefault();
        }

        public void AddPerson(Person person)
        {
            var context = new ValiasrContext("Valiasr.ce");
            Person correspondent = person;
            context.Persons.Add(correspondent);
            context.SaveChanges();

        }

        [Test]
        public void AddCustomer(
            [Values("1")] string melliIdentity,
            [Values("5")]string no,
            [Values(1)] float portion)
        {
            var context = new ValiasrContext("Valiasr.ce");
            Person person = (from p in context.Persons where p.MelliIdentity == melliIdentity select p).FirstOrDefault();

            var customer = new Customer
                {
                    ContactInfo =  person.ContactInfo,
                    CustomerId = person.CustomerId,
                    ShobehCode = person.ShobehCode,
                    Firstname = person.Firstname,
                    Lastname = person.Lastname,
                    HeadMelliIdentity = melliIdentity,
                    No = no,
                    Portion = portion,
                    Id = person.Id
                };

            Account account = (from c in context.Accounts where c.Hesab_No == "1/0/2" select c).FirstOrDefault();
            account.Persons.Add(customer);
            //context.Accounts.Add(account);
            //context.Persons.Add(correspondent);
            context.Entry(person).State = EntityState.Unchanged;
            context.SaveChanges();
        }
    }
}
