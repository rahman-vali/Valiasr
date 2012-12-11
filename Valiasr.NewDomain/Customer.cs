namespace Valiasr.NewDomain
{
    using System;
    using System.Collections.Generic;

    public class Person
    {
        public Person()
        {
            this.ContactInfo = new ContactInfo();
            BirthDate =  DateTime.Now;
        }
        public Guid Id { get; set; }

        public DateTime BirthDate { get; set; }

        public bool Balegh
        {
            get
            {
                if ((DateTime.Now - BirthDate).TotalDays / 365 < 18)
                    return false;
                else
                    return true;
            }
        }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }

    public class Correspondent:Person
    {
         
    }

    public class Customer : Correspondent
    {
        public ICollection<Account> Accounts { get; set; }

        public bool HagheBardasht { get; set; }

        public float Portion { get; set; }
    }

    public class Vakil : Correspondent
    {
        public DateTime? EndDate { get; set; }

        public DateTime StartDate { get; set; }
    }

    //    public class Zamen : Customer
    //    {
    //        public ICollection<Vam> Vams { get; set; }
    //    }
}