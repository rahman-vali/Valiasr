namespace Valiasr.Service
{
    using System;
    using System.Linq;

    using Valiasr.DataAccess.Repositories;
    using Valiasr.Domain.Model;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
     
    public class PersonService : IPersonService
     {

        public PersonDto GetPerson(string nationalIdentity)
        {
            PersonRepository personRepository = new PersonRepository();
            Person person = personRepository.GetPerson(nationalIdentity);
            if (person != null)
                return TranslatePersonToPersonDto(person);
            return null;
        }

        public void AddPerson(PersonDto personDto)
        {    
            Person person = new Person();
            TranslatePersonDtoToPerson(personDto,person);        
            PersonRepository repository = new PersonRepository();
            repository.Add(person);
             
        }


        public void UpdatePerson(PersonDto personDto)
        {
            string nationalIdentity = personDto.NationalIdentity;
            PersonRepository repository = new PersonRepository();
            Person person = repository.ActiveContext.Persons.Where(p => p.NationaliIdentity == nationalIdentity).FirstOrDefault();
            if (person == null)
                throw new Exception("no perso with this National Identity  " + nationalIdentity);
            TranslatePersonDtoToPerson(personDto , person);

            repository.Update(person);

        }

        public void AddCustomer(string melliIdentity, string no, float portion)
        {
            CustomerRepository repository = new CustomerRepository();           
            repository.AddCustomer( melliIdentity, no , portion);
        }

        public void AddLawyer(string melliIdentity, DateTime startDate)
        {
            LawyerRepository repository = new LawyerRepository();
            repository.AddLawyer(melliIdentity , startDate);
        }

        private PersonDto TranslatePersonToPersonDto(Person person)
        {
           PersonDto personDto = new PersonDto{
                   CustomerId = person.CustomerId,
                   ShobehCode = person.ShobehCode,
                   Firstname = person.Firstname,
                   Lastname = person.Lastname,
                   FatherName = person.FatherName,
                   CretyId = person.CretyId,
                   CretySerial = person.CretySerial,
                   Sadereh = person.Sadereh,
                   BirthDate = person.BirthDate,
                   NationalIdentity = person.NationaliIdentity,
                   HeadNationalIdentity = person.HeadNationalIdentity,
                   JobName = person.JobName,
                   JobKind = person.JobKind,
                   Salary = person.Salary,
                   IndivOrOrgan = person.IndivOrOrgan,
                   HomeAddress = person.ContactInfo.HomeAddress,
                   WorkAddress = person.ContactInfo.WorkAddress,
                   HomeTelno = person.ContactInfo.HomeTelno,
                   OfficeTelNo = person.ContactInfo.OfficeTelNo,
                   Mobile = person.ContactInfo.Mobile,
                   PostalIdentity = person.ContactInfo.PostalIdentity,                  
               };
            return personDto;
        }

        /*private Person TranslatePersonDtoToPerson(PersonDTO personDto )
        {
            Person person = new Person {
                   CustomerId = personDto.CustomerId,
                   ShobehCode = personDto.ShobehCode,
                   Firstname = personDto.Firstname,
                   Lastname = personDto.Lastname,
                   FatherName = personDto.FatherName,
                   CretyId = personDto.CretyId,
                   CretySerial = personDto.CretySerial,
                   Sadereh = personDto.Sadereh,
                   NationaliIdentity = personDto.NationalIdentity,
                   HeadNationalIdentity = personDto.HeadNationalIdentity,
                   JobName = personDto.JobName,
                   JobKind = personDto.JobKind,
                   Salary = personDto.Salary,
                   IndivOrOrgan = personDto.IndivOrOrgan,
                   ContactInfo = new ContactInfo {
                           HomeAddress = personDto.HomeAddress,
                           WorkAddress = personDto.WorkAddress,
                           HomeTelno = personDto.HomeTelno,
                           OfficeTelNo = personDto.OfficeTelNo,
                           Mobile = personDto.Mobile,
                           PostalIdentity = personDto.PostalIdentity,                  
                       },
              };
            return person;

        }*/

        private void TranslatePersonDtoToPerson(PersonDto personDto , Person person)
        {
            person.CustomerId = personDto.CustomerId;
            person.ShobehCode = personDto.ShobehCode;
            person.Firstname = personDto.Firstname;
            person.Lastname = personDto.Lastname;
            person.FatherName = personDto.FatherName;
            person.CretyId = personDto.CretyId;
            person.CretySerial = personDto.CretySerial;
            person.Sadereh = personDto.Sadereh;
            person.NationaliIdentity = personDto.NationalIdentity;
            person.HeadNationalIdentity = personDto.HeadNationalIdentity;
            person.JobName = personDto.JobName;
            person.JobKind = personDto.JobKind;
            person.Salary = personDto.Salary;
            person.ContactInfo.HomeAddress = personDto.HomeAddress;
            person.ContactInfo. WorkAddress = personDto.WorkAddress;
            person.ContactInfo.HomeTelno = personDto.HomeTelno;
            person.ContactInfo.OfficeTelNo = personDto.OfficeTelNo;
            person.ContactInfo.Mobile = personDto.Mobile;
            person.ContactInfo.PostalIdentity = personDto.PostalIdentity;           
        }

     }
   
}
