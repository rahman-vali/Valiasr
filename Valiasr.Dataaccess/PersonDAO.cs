namespace Valiasr.DataAccess
{
    using System;
    using System.Linq;

    using Valiasr.Domain;

    public class PersonDAO
    {
        public Correspondent GetPerson(string melliIdentity)
        {
            var context = new ValiasrContext("Valiasr.ce");
            return (from p in context.Correspondents where p.MelliIdentity == melliIdentity select p).FirstOrDefault();
        }

        public Correspondent GetAcount(string melliIdentity)
        {
            var context = new ValiasrContext("Valiasr.ce");
            return (from p in context.Correspondents where p.MelliIdentity == melliIdentity select p).FirstOrDefault();
        }

        public void AddPerson(Correspondent person)
        {
            var context = new ValiasrContext("Valiasr.ce");
            Correspondent correspondent = person;
            context.Correspondents.Add(correspondent);
            context.SaveChanges();

        }
        public void AddCustomer(string melliIdentity, string no, float portion)
        {
            var context = new ValiasrContext("Valiasr.ce");
            Correspondent correspondent = (from p in context.Correspondents where p.MelliIdentity == melliIdentity select p).FirstOrDefault();
            var person = (Person)correspondent;

            Customer customer =
                Customer.CreateCustomer(
                correspondent.Firstname,
                correspondent.Lastname,
                correspondent.ContactInfo.HomeAddress,
                no,
                melliIdentity);
            if (correspondent != null)
                customer.Id = correspondent.Id;
            customer.No = no;
            customer.Portion = portion;

            Account account = (from c in context.Accounts where c.Hesab_No == "1/0/2" select c).FirstOrDefault();
            account.Correspondents.Add(customer);
            //context.Accounts.Add(account);
            //context.Correspondents.Add(correspondent);
            context.SaveChanges();
        }
    }
}
