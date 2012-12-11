namespace Valiasr.Domain
{
    using System;
    using System.Collections.Generic;

    public class Customer
    {
        public Customer()
        {
            this.ContactInfo = new ContactInfo();
        }
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public  ICollection<CustomerHesab> CustomerHesabs { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }
}
