namespace Valiasr.DataAccess
{
    using System.Data;
    using System.Linq;

    using Valiasr.Domain;

    public class PersonRepository
    {
        private readonly ValiasrContext context = new ValiasrContext("Valiasr.ce");

        public Person GetPerson(string melliIdentity)
        {
            return (from p in context.Persons where p.MelliIdentity == melliIdentity select p).FirstOrDefault();
        }

        public Person GetAcount(string melliIdentity)
        {
            return (from p in context.Persons where p.MelliIdentity == melliIdentity select p).FirstOrDefault();
        }

        public void AddPerson(Person person)
        {
            Person correspondent = person;
            context.Persons.Add(correspondent);
            context.SaveChanges();
        }

        public void AddCustomer(string melliIdentity, string no, float portion)
        {
            Person person = (from p in context.Persons where p.MelliIdentity == melliIdentity select p).FirstOrDefault();

            var customer = new Customer
                {
                    Person = new Person()
                    {
                            ContactInfo = person.ContactInfo,
                            CustomerId = person.CustomerId,
                            ShobehCode = person.ShobehCode,
                            Firstname = person.Firstname,
                            Lastname = person.Lastname,
                            HeadMelliIdentity = melliIdentity,
                    },
                    No = no,
                    Portion = portion,
                    Id = person.Id
                };

            Account account = (from c in context.Accounts where c.Code == "1/0/2" select c).FirstOrDefault();
            account.Customers.Add(customer);
            context.Entry(person).State = EntityState.Unchanged;
            context.SaveChanges();
        }
    }
}