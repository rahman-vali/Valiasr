namespace Valiasr.Domain.Model
{
    public class ContactInfo
    {
        public string HomeAddress { get; set; }

        public string WorkAddress { get; set; }

        public string  HomeTelno { get; set; }

        public string OfficeTelNo { get; set; }

        public string Mobile { get; set; }

        public string PostalIdentity { get; set; }

        public bool HasValue()
        {
            return (this.HomeAddress != null || this.HomeTelno != "0");
        }
    }
}