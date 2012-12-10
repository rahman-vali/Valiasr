using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valiasr.Domain
{
    public class CustomerHesab
    {
        public Guid Id { get; set; }
        public Role Role { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Account Account { get; set; }

    }
}
