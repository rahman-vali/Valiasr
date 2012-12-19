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
            PersonDAO personDAO = new PersonDAO();
            Person person = personDAO.GetPerson(melliIdentity); 
            if (person != null)
              TranslatePersonToPersonDTO(person , personDTO);
            else
              personDTO = null;            
            return personDTO;
        }

        private void TranslatePersonToPersonDTO(Person person, PersonDTO personDTO)
        {
            personDTO.CustomerId = person.CustomerId;
            personDTO.ShobehCode = person.ShobehCode;
            personDTO.Firstname = person.Firstname;
            personDTO.Lastname = person.Lastname;
            personDTO.FatherName = person.FatherName;
            personDTO.CretyId = person.CretyId;
            personDTO.CretySerial = person.CretySerial;
            personDTO.Sadereh = person.Sadereh;
            personDTO.BirthDate = person.BirthDate;
            personDTO.MelliIdentity = person.MelliIdentity;
            personDTO.HeadMelliIdentity = person.HeadMelliIdentity;
            personDTO.JobName = person.JobName;
            personDTO.JobKind = person.JobKind;
            personDTO.Salary = person.Salary;
            personDTO.IndivOrOrgan = person.IndivOrOrgan;
            personDTO.HomeAddress = person.ContactInfo.HomeAddress;
            personDTO.WorkAddress = person.ContactInfo.WorkAddress;
            personDTO.HomeTelno = person.ContactInfo.HomeTelno;
            personDTO.OfficeTelNo = person.ContactInfo.OfficeTelNo;
            personDTO.Mobile = person.ContactInfo.Mobile;
            personDTO.PostIdentity = person.ContactInfo.PostIdentity;
        }



     }
   
}
