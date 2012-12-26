namespace Valiasr.Domain
{
    using System;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Moshtari
    /// </summary>
    public class Customer
    {
        public Customer()
        {
            Person= new Person();
            Accounts = new Collection<Account>();
        }
        public Guid Id { get; set; }
        public Person Person { get; set; }
        public string No { get; set; }
        public bool HagheBardasht { get; set; }
        public float Portion { get; set; }
        public Collection<Account> Accounts { get; set; }
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