namespace Valiasr.Domain.Model
{
    using System;

    public class Person:IAggregateRoot
    {
        public Person()
        {
            Id = Guid.NewGuid();
            ContactInfo = new ContactInfo();
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
        public string NationaliIdentity { get; set; }
        public string HeadNationalIdentity { get; set; }
        public string JobName { get; set; }
        public short JobKind { get; set; }
        public decimal Salary { get; set; }
        public int RegPerId { get; set; }
        public int LastPerId { get; set; }
        public short IndivOrOrgan { get; set; }
        public int LastDate { get; set; }
        public ContactInfo ContactInfo { get; set; }


        public bool CanBeSaved
        {
            get { return true ; }
        }

        public bool CanBeDeleted
        {
            get { return true ; }
        }
    }
}
/*        public static Person CreatePerson(int shobehCode ,int customerId , string firstname , string lastname , string fatherName , string cretyId , string cretySerial ,
            string sadereh , int birthDate , string nationaliIdentity , string headNationalIdentity , string jobName , short jobKind , decimal salary , int regPerId , 
            int lastPerId , string indivOrOrgan , int lastDate , string homeAddress , string workAddress , string  homeTelno , string officeTelNo , string mobile , 
            string postalIdentity)
        {
            return new Person
            {
                Id = Guid.NewGuid(), ShobehCode = shobehCode , CustomerId = customerId , Firstname = firstname , Lastname = lastname , FatherName = fatherName , 
                CretyId = cretyId , CretySerial = cretySerial , Sadereh = sadereh , BirthDate = birthDate , NationaliIdentity = nationaliIdentity , 
                HeadNationalIdentity = headNationalIdentity , JobName = jobName , JobKind = jobKind , Salary = salary , RegPerId = regPerId , LastPerId = lastPerId , 
                IndivOrOrgan = indivOrOrgan , LastDate = lastDate, 
                ContactInfo = new ContactInfo { HomeAddress = homeAddress , WorkAddress = workAddress , HomeTelno = homeTelno , OfficeTelNo = officeTelNo , Mobile = mobile, 
                PostalIdentity = postalIdentity}
            };
        }*/
