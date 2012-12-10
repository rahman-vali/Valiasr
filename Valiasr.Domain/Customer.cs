using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valiasr.Domain
{
    public class Customer
    {
        public Customer()
        {
            ContactInfo = new ContactInfo();
        }
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public virtual  ICollection<CustomerHesab> CustomerHesabs { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }
}
