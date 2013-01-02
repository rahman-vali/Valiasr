using System;
using System.Linq;

namespace Valiasr.Service
{
    using Valiasr.DataAccess.Repositories;
    using Valiasr.Domain.Model;

    public class PersonAccountService:IPersonAccountService
    {
        /// <summary>
        /// person region
        /// </summary>
        /// <param name="personDto"></param>
        public void AddPerson(PersonDto personDto)
        {
            Person person = new Person();
            TranslatePersonDtoToPerson(personDto, person);
            PersonRepository repository = new PersonRepository();
            repository.Add(person);

        }

        public void RemovePerson(Guid id)
        {
            /*if (id != null)
            {
                if (int count = (from p in ))
            }*/
        }

        public void UpdatePerson(PersonDto personDto)
        {
            Guid id = personDto.Id;
            PersonRepository repository = new PersonRepository();
            Person person = repository.ActiveContext.Persons.Where(p => p.Id == id).FirstOrDefault();
            if (person == null)
                throw new Exception("no person with this National Identity:  " + personDto.NationalIdentity);
            TranslatePersonDtoToPerson(personDto, person);
            repository.Update(person);

        }

        public PersonDto GetPerson(string nationalIdentity)
        {
            PersonRepository repository = new PersonRepository();
            Person person = repository.ActiveContext.Persons.Where(p => p.NationaliIdentity == nationalIdentity).FirstOrDefault();
            if (person == null)
                throw new Exception("no perso with this National Identity  " + nationalIdentity);
            return TranslatePersonToPersonDto(person);            
        }

        public void AddCustomer(CustomerDto customerDto)
        {
            Guid id = customerDto.PersonId;
            CustomerRepository repository = new CustomerRepository();
            Person person = repository.ActiveContext.Persons.Where(p => p.Id == id).FirstOrDefault();
            if (person == null)
                throw  new Exception("the person id is invalid and can't be find");
            Customer customer = Customer.CreateCustomer(person, customerDto.No, customerDto.Portion);
            repository.Add(customer);
        }

        public void AddLawyer(LawyerDto lawyerDto)
        {
            LawyerRepository repository = new LawyerRepository();
            Person person = repository.ActiveContext.Persons.Where(p => p.Id == lawyerDto.PersonId).FirstOrDefault();
            if (person == null)
                throw new Exception("the person id is invalid and can't be find");
            repository.Add(Lawyer.CreateLawyer(person , lawyerDto.StartDate));
        }

        private PersonDto TranslatePersonToPersonDto(Person person)
        {
            PersonDto personDto = new PersonDto
            {
                Id = person.Id,
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


        private void TranslatePersonDtoToPerson(PersonDto personDto, Person person)
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
            person.ContactInfo.WorkAddress = personDto.WorkAddress;
            person.ContactInfo.HomeTelno = personDto.HomeTelno;
            person.ContactInfo.OfficeTelNo = personDto.OfficeTelNo;
            person.ContactInfo.Mobile = personDto.Mobile;
            person.ContactInfo.PostalIdentity = personDto.PostalIdentity;
        }


       /// <summary>
       /// accout region
       /// </summary>
       /// <param name="generalAccountDto"></param>

        public void AddGeneralAccount(GeneralAccountDto generalAccountDto)
        {
            GeneralAccount generalAccount = GeneralAccount.CreateGeneralAccount(
                generalAccountDto.Code, generalAccountDto.Description, generalAccountDto.Category);
            GeneralAccountRepository repository = new GeneralAccountRepository();
            repository.Add(generalAccount);
        }


        public void AddIndexAccount(IndexAccountDto indexAccountDto)
        {
            IndexAccountRepository repository = new IndexAccountRepository();
            GeneralAccount generalAccount =
                repository.ActiveContext.GeneralAccounts.Where(ga => ga.Id == indexAccountDto.GeneralAccountId).FirstOrDefault();
            IndexAccount indexAccount = IndexAccount.CreateIndexAccount(
                generalAccount,
                indexAccountDto.Code,
                indexAccountDto.GeneralAccountCode,
                indexAccountDto.Indexer,
                indexAccountDto.Description,
                indexAccountDto.HaveAccounts);
            repository.Add(indexAccount);
        }


        public void AddAccount(AccountDto accountDto)
        { 
            
            AccountRepository repository = new AccountRepository();
            IndexAccount indexAccount =
                repository.ActiveContext.IndexAccounts.Where(ia => ia.Id == accountDto.IndexAccountId)
                          .FirstOrDefault();
            Account account = Account.CreateAccount(
                indexAccount,
                accountDto.Code,
                accountDto.No,
                accountDto.Indexer,
                accountDto.Balance,
                accountDto.Description);
            repository.Add(account);
        }
    }
}

#region IPerson
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

#endregion
#region IAccountService

/* public bool Bardasht(string account, double amount)
        {
            throw new NotImplementedException();
        }

        public bool CanBardasht(string accountNo, double amount)
        {
            Account acc = this.context.Accounts.Include("RelativePersons").FirstOrDefault(o => o.No == accountNo);
            if (acc == null)
            {
                throw new ArgumentNullException("Customer Does Not Exist");
            }
            return acc.Bardasht(accountNo, amount);
        }

        public ICollection<CustomerDto> GetCustomerByAccountNo(string accountNo)
        {
            return
                this.context.Accounts.Include("Customers")
                    .Where(c => c.No == accountNo)
                    .SelectMany(o => o.Customers)
                    .Select(o => new CustomerDto { HagheBardasht = o.HagheBardasht, No = o.No, Portion = o.Portion })
                    .ToList();
        }*/

#endregion
