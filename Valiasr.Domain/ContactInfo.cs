using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Valiasr.Domain
{
    [ComplexType]
    public class ContactInfo
    {
        public string Address { get; set; }
        public int Tellno { get; set; }
        public bool HasValue ()
        {
            return (Address != null || Tellno != 0);
        }
    }

}
