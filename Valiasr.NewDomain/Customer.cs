namespace Valiasr.NewDomain
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    public class Person
    {
        public Person()
        {
            Id = Guid.NewGuid();
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
        public ICollection<Account> Accounts { get; set; }  
    }

    public class Customer : Correspondent
    {
        protected bool Equals(Customer other)
        {
            return string.Equals(this.No, other.No) && this.HagheBardasht.Equals(other.HagheBardasht) && this.Portion.Equals(other.Portion);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (this.No != null ? this.No.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Accounts != null ? this.Accounts.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ this.HagheBardasht.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Portion.GetHashCode();
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((Customer)obj);
        }

        public string No { get; set; }

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