namespace Valiasr.Domain
{
    using System;
    using System.Runtime.Serialization;

    public class Customer : Correspondent
    {
       /* public Customer()
        {
            
        }*/
        public string No { get; set; }

        public bool HagheBardasht { get; set; }

        public float Portion { get; set; }

        public static Customer CreateCustomer(string fName, string lName, string address ,string no , string melliIdentity)
        {   
            var customer = new Customer() {Firstname = fName, Lastname = lName ,No = no ,MelliIdentity = melliIdentity};
            
            customer.ContactInfo = new ContactInfo(){HomeAddress = address};

            return customer;                                    
        }
    }

    public class Vakil : Correspondent
    { 
        public DateTime? EndDate { get; set; }

        public DateTime   StartDate { get; set; }

        public static Vakil CreateVakil(string fName, string lName, string address, DateTime startDate , string melliIdentity)
        {
            var vakil = new Vakil() {Firstname = fName, Lastname = lName, StartDate = startDate , MelliIdentity = melliIdentity};

            vakil.ContactInfo = new ContactInfo() { HomeAddress = address };

            return vakil;
        }

    }

    //    public class Zamen : Customer
    //    {
    //        public ICollection<Vam> Vams { get; set; }
    //    }
}