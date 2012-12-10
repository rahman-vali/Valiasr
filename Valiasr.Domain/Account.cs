using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valiasr.Domain
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Hesab_Des { get; set; }
        public string Sub_Des { get; set; }

        public virtual ICollection<CustomerHesab> CustomerHesabs { get; set; }
    }
}
