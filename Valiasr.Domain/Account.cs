namespace Valiasr.Domain
{
    using System;
    using System.Collections.Generic;

    public class Account
    {
        public Guid Id { get; set; }
        public string Hesab_Des { get; set; }
        public string Sub_Des { get; set; }
        public ICollection<CustomerHesab> CustomerHesabs { get; set; }
    }
}
