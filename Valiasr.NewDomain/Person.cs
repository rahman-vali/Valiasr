namespace Valiasr.NewDomain
{
    using System;

    public class Person
    {
        public Person()
        {
            this.Id = Guid.NewGuid();
            this.ContactInfo = new ContactInfo();
            this.BirthDate =  DateTime.Now;
        }
        public Guid Id { get; set; }

        public DateTime BirthDate { get; set; }

        public bool Balegh
        {
            get
            {
                if ((DateTime.Now - this.BirthDate).TotalDays / 365 < 18)
                    return false;
                else
                    return true;
            }
        }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }
}