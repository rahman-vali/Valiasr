namespace Valiasr.Domain
{
    using System;

    public class Person
    {
        public Person()
        {
            Id = Guid.NewGuid();
        }

        public bool Balegh
        {
            get
            {
                /*  if ((DateTime.Now - this.BirthDate).TotalDays / 365 < 18)
                      return false;
                  else*/
                return true;
            }
        }
        public Guid Id { get; set; }
        public int CustomerId { get; set; }
        public int ShobehCode { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string FatherName { get; set; }
        public string CretyId { get; set; }
        public string CretySerial { get; set; }
        public string Sadereh { get; set; }
        public int BirthDate { get; set; }
        public string MelliIdentity { get; set; }
        public string HeadMelliIdentity { get; set; }
        public string JobName { get; set; }
        public short JobKind { get; set; }
        public decimal Salary { get; set; }
        public int RegPerId { get; set; }
        public int LastPerId { get; set; }
        public string IndivOrOrgan { get; set; }
        public int LastDate { get; set; }
        public ContactInfo ContactInfo { get; set; }

    }
}