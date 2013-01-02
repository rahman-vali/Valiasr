namespace Valiasr.Domain.Model
{
    using System;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Moshtari
    /// </summary>
    public class Customer:IAggregateRoot
    {
        public Customer()
        {
            Person= new Person();
            Accounts = new Collection<Account>();
        }

        public static Customer CreateCustomer(Person person,string no , float portion)
        {
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
        public string No { get; set; }
        public bool HagheBardasht { get; set; }
        public float Portion { get; set; }
        public Collection<Account> Accounts { get; set; }

        public bool CanBeSaved
        {
            get { return true ; }
        }

        public bool CanBeDeleted
        {
            get { return true ; }
        }
    }

    /// <summary>
    /// Vakil
    /// </summary>
    public class Lawyer:IAggregateRoot 
    {
        public Lawyer()
        {
            Person = new Person();
            Accounts = new Collection<Account>();
        }

        public static Lawyer CreateLawyer(Person person , DateTime startDate)
        {
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


        public bool CanBeSaved
        {
            get { return true ; }
        }

        public bool CanBeDeleted
        {
            get { return true ; }
        }
    }

    //    public class Zamen : Customer
    //    {
    //        public Collection<Vam> Vams { get; set; }
    //    }
}