namespace Valiasr.Domain.Model
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Moshtari
    /// </summary>
    public class Customer
    {
        public Customer()
        {
            Person= new Person();
   //         Accounts = new Collection<Account>();
        }

        public static Customer CreateCustomer(Person person,string no , float portion)
        {
            //Contract.Requires<ArgumentNullException>(person != null, "person");
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Person = person,
                No = no,
                Portion = portion,
            };
            return customer;
        }

        public Guid Id { get; set; }
        public Person Person { get; set; }
 //       [StringLenghValidator(20)]
        public string No { get; set; }
        public bool HagheBardasht { get; set; }
        public float Portion { get; set; }
        public Collection<Account> Accounts { get; set; }
        public bool ContainAccount()
        {
            var collection = this.Accounts;
            return (collection != null) && (collection.Count != 0);
        }        
    }

    /// <summary>
    /// Vakil
    /// </summary>
    public class Lawyer 
    {
        public Lawyer()
        {
            Person = new Person();
            Accounts = new Collection<Account>();
        }

        public static Lawyer CreateLawyer(Person person , DateTime startDate)
        {
           // Contract.Requires<ArgumentNullException>(person != null ,"person");
            var lawyer = new Lawyer
            {
                Id = Guid.NewGuid(),
                StartDate = startDate,
                Person = person,
            };
            return lawyer;
        }

        public Guid Id { get; set; }
        public Person Person { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public Collection<Account> Accounts { get; set; }
    }

    //    public class Zamen : Customer
    //    {
    //        public Collection<Vam> Vams { get; set; }
    //    }
}