namespace Valiasr.NewDomain
{
    public class ContactInfo
    {
        public string Address { get; set; }
        public int Tellno { get; set; }
        public bool HasValue()
        {
            return (this.Address != null || this.Tellno != 0);
        }
    }
}