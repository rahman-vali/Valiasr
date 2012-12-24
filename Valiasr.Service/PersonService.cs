using System.Linq;

namespace Valiasr.Service
{
    using Valiasr.DataAccess;
    using Valiasr.Domain;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
     
    public class PersonService : IPersonService
     {
        public PersonDTO GetPerson(string melliIdentity)
        {
            PersonDTO personDTO =  new PersonDTO();           
            PersonDAO personDao = new PersonDAO();
            Correspondent person = personDao.GetPerson(melliIdentity);
            if (person != null)
                TranslatePersonToPersonDto(person, personDTO);
            else
                return null;//personDTO = null;            
            return personDTO;
        }
         public void AddPerson(PersonDTO personDto)
         {
             Customer person = Customer.CreateCustomer(personDto.Firstname, personDto.Lastname, personDto.HomeAddress, "12", personDto.MelliIdentity); 
            Correspondent correspondent = person;// vakil;//new Correspondent();
            PersonDAO personDao = new PersonDAO();
             
             TranslatePersonDtoToPerson(personDto, person);
             personDao.AddPerson(person);

         }
        private void TranslatePersonToPersonDto(Correspondent person, PersonDTO personDto)
        {
            personDto.CustomerId = person.CustomerId;
            personDto.ShobehCode = person.ShobehCode;
            personDto.Firstname = person.Firstname;
            personDto.Lastname = person.Lastname;
            personDto.FatherName = person.FatherName;
            personDto.CretyId = person.CretyId;
            personDto.CretySerial = person.CretySerial;
            personDto.Sadereh = person.Sadereh;
            personDto.BirthDate = person.BirthDate;
            personDto.MelliIdentity = person.MelliIdentity;
            personDto.HeadMelliIdentity = person.HeadMelliIdentity;
            personDto.JobName = person.JobName;
            personDto.JobKind = person.JobKind;
            personDto.Salary = person.Salary;
            personDto.IndivOrOrgan = person.IndivOrOrgan;
            personDto.HomeAddress = person.ContactInfo.HomeAddress;
            personDto.WorkAddress = person.ContactInfo.WorkAddress;
            personDto.HomeTelno = person.ContactInfo.HomeTelno;
            personDto.OfficeTelNo = person.ContactInfo.OfficeTelNo;
            personDto.Mobile = person.ContactInfo.Mobile;
            personDto.PostIdentity = person.ContactInfo.PostIdentity;
        }

        private void TranslatePersonDtoToPerson(PersonDTO personDto , Correspondent person)
        {
            person.CustomerId = personDto.CustomerId;
            person.ShobehCode = personDto.ShobehCode;
            person.Firstname = personDto.Firstname;
            person.Lastname = personDto.Lastname;
            person.FatherName = personDto.FatherName;
            person.CretyId = personDto.CretyId;
            person.CretySerial = personDto.CretySerial;
            person.Sadereh = personDto.Sadereh;
            person.MelliIdentity = personDto.MelliIdentity;
            person.HeadMelliIdentity = personDto.HeadMelliIdentity;
            person.JobName = personDto.JobName;
            person.JobKind = personDto.JobKind;
            person.Salary = personDto.Salary;
            person.IndivOrOrgan = personDto.IndivOrOrgan;
            person.ContactInfo.HomeAddress = personDto.HomeAddress;
            person.ContactInfo.WorkAddress = personDto.WorkAddress;
            person.ContactInfo.HomeTelno = personDto.HomeTelno;
            person.ContactInfo.OfficeTelNo = personDto.OfficeTelNo;
            person.ContactInfo.Mobile = personDto.Mobile;
            person.ContactInfo.PostIdentity = personDto.PostIdentity;
        }


     }
   
}
