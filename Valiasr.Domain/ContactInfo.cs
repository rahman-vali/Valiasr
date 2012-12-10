using System.ComponentModel.DataAnnotations.Schema;

namespace Valiasr.Domain
{
    public class ContactInfo
    {
        public string Address { get; set; }
        public int Tellno { get; set; }
        public bool HasValue()
        {
            return (Address != null || Tellno != 0);
        }
    }

}
