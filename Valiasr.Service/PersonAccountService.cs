using System;
using System.Linq;

namespace Valiasr.Service
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;

    using Omu.ValueInjecter;

    using Valiasr.DataAccess.Repositories;
    using Valiasr.Domain.Model;

    public partial class PersonAccountService : IPersonAccountService
    {
        /// <summary>
        /// person region
        /// </summary>
        /// <param name="personDto"></param>
        public string AddPerson(PersonDto personDto)
        {
            try
            {
                string nationalIdentity = personDto.NationalIdentity;
                PersonRepository repository = new PersonRepository();
                if (repository.ActiveContext.Persons.Where(p => p.NationalIdentity == nationalIdentity).Count() != 0) return "person with this natinal identity is there ";
                personDto.CustomerId =
                    repository.ActiveContext.Persons.Select(o => o.CustomerId).DefaultIfEmpty(0).Max() + 1;
                personDto.Id = Guid.NewGuid();
                Person person = new Person();
                person.InjectFrom<UnflatLoopValueInjection>(personDto);
                person.ContactInfo.InjectFrom<UnflatLoopValueInjection>(personDto);
                repository.Add(person);
                return "person is added successfully by customerId :" + personDto.CustomerId.ToString();
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public string UpdatePerson(PersonDto personDto)
        {
            try
            {
                Guid id = personDto.Id;
                PersonRepository repository = new PersonRepository();
                Person person = repository.ActiveContext.Persons.Where(p => p.Id == id).FirstOrDefault();
                if (person == null) return ("person is not found and will not be updated ");
                person.InjectFrom<UnflatLoopValueInjection>(personDto);
                person.ContactInfo.InjectFrom<UnflatLoopValueInjection>(personDto);
                repository.Update(person);
                return ("updated successfully");
            }
            catch (Exception exception)
            {
                return exception.Message;
            }

        }

        public string RemovePerson(Guid id)
        {
            try
            {
                if (id == Guid.Empty) return ("person's id is not vali and can not be deleted");
                PersonRepository repository = new PersonRepository();
                Person person = repository.ActiveContext.Persons.Where(p => p.Id == id).FirstOrDefault();
                if (person == null) return ("person is not there in database");
                string messageStr = "";
                if (repository.PersonIsCustomerOrLawyer(id, ref messageStr)) return messageStr;
                repository.Remove(person);
                return ("person successfully removed");
            }
            catch (Exception fException)
            {
                return fException.Message;
            }
        }


        public PersonDto GetPersonByNationalIdentity(string nationalIdentity)
        {
            PersonRepository repository = new PersonRepository();
            var person = repository.ActiveContext.Persons
                .Where(p => p.NationalIdentity == nationalIdentity).FirstOrDefault();
            return person != null
                       ? (PersonDto)
                         new PersonDto().InjectFrom<UnflatLoopValueInjection>(person).InjectFrom(person.ContactInfo)
                       : null;
        }


        public List<PersonDto> GetPersonByAccount(string code)
        {
            PersonRepository repository = new PersonRepository();
            Account account =
                repository.ActiveContext.BankAccounts.OfType<Account>()
                          .Include("Customers.Person")
                          .Include("Lawyers.Person")
                          .FirstOrDefault(a => a != null && a.Code == code);


            if (account != null && ((account.Customers.Count != 0) || (account.Lawyers.Count != 0)))
            {
                var personDtos =
                    account.Customers.Select(a => new PersonDto().InjectFrom(a.Person).InjectFrom(a.Person.ContactInfo))
                           .Union(account.Lawyers.Select(l => new PersonDto().InjectFrom(l.Person).InjectFrom(l.Person.ContactInfo)));
                return personDtos.OfType<PersonDto>().ToList();
            }
            return null;
        }

        public PersonDto GetPersonById(Guid id)
        {
            PersonRepository repository = new PersonRepository();
            var person = repository.ActiveContext.Persons
                .Where(p => p.Id == id).FirstOrDefault();
            return person != null
                       ? (PersonDto)
                         new PersonDto().InjectFrom<UnflatLoopValueInjection>(person).InjectFrom(person.ContactInfo)
                       : null;
        }
        public string AddCustomerToAccount(Guid accountId, CustomerDto customerDto)
        {
            try
            {
                Guid personId = customerDto.PersonId;
                AccountRepository repository = new AccountRepository();
                Person person = repository.ActiveContext.Persons.Where(p => p.Id == personId).FirstOrDefault();
                if (person == null) return ("the person id is invalid and customer can't be find");
                Account account =
                    repository.ActiveContext.BankAccounts.OfType<Account>()
                              .Include("customers.Person")
                              .Where(a => a.Id == accountId)
                              .FirstOrDefault();
                if (account == null) return "account id is invalid and can not be find";
                if (!account.ContainCustomer(personId))
                {
                    Customer customer = Customer.CreateCustomer(person, customerDto.No, customerDto.Portion);
                    account.Customers = new Collection<Customer>();
                    account.Customers.Add(customer);
                    repository.ActiveContext.SaveChanges();
                    return "customer added successfully";
                }
                return "customer was there in database";
            }
            catch (Exception fException)
            {
                return fException.Message;
            }
        }

        public string AddLawyerToAccount(Guid accountId, LawyerDto lawyerDto)
        {
            try
            {
                Guid personId = lawyerDto.PersonId;
                AccountRepository repository = new AccountRepository();
                Person person = repository.ActiveContext.Persons.Where(p => p.Id == personId).FirstOrDefault();
                if (person == null) return ("the person id is invalid and person can't be find");
                Account account =
                    repository.ActiveContext.BankAccounts.OfType<Account>()
                              .Include("Lawyers.Person")
                              .FirstOrDefault(a => a.Id == accountId);
                if (account == null) return "account id is invalid and lawyer can't be added";
                if (!account.ContainLawyer(personId))
                {
                    Lawyer lawyer = Lawyer.CreateLawyer(person, lawyerDto.StartDate);
                    account.Lawyers = new Collection<Lawyer>();
                    account.Lawyers.Add(lawyer);
                    repository.ActiveContext.SaveChanges();
                    return "lawyer added successfully";
                }
                return "lawyer was there and can't be recreated";
            }
            catch (Exception fException)
            {
                return fException.Message;
            }
        }



        /// <summary>
        /// accout region
        /// </summary>
        /// <param name="generalAccountDto"></param>

        public string AddGeneralAccount(GeneralAccountDto generalAccountDto)
        {
            try
            {
                int code = generalAccountDto.Code;
                GeneralAccountRepository repository = new GeneralAccountRepository();
                if ((repository.ActiveContext.GeneralAccounts.Count(ga => ga.Code == code)) == 0)
                {
                    GeneralAccount generalAccount = new GeneralAccount();
                    generalAccountDto.Id = Guid.NewGuid();
                    generalAccount.InjectFrom<UnflatLoopValueInjection>(generalAccountDto);
                    repository.Add(generalAccount);
                    return "has successfully created";
                }
                return "this record with this code is there";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public string RemoveGeneralAccount(Guid id)
        {
            try
            {
                GeneralAccountRepository repository = new GeneralAccountRepository();
                GeneralAccount generalAccount =
                    repository.ActiveContext.GeneralAccounts.Include("IndexAccounts")
                              .Where(ga => ga.Id == id)
                              .FirstOrDefault();
                if (generalAccount == null) return "no record was found by this id";
                if (generalAccount.ContainIndexAccounts) return "this record has index account record and can not be deleted";
                repository.Remove(generalAccount);
                return "has successfully removed";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }


        public GeneralAccountDto GetGeneralAccount(int code)
        {
            GeneralAccountRepository repository = new GeneralAccountRepository();
            GeneralAccount general = repository.ActiveContext.GeneralAccounts
                .Where(ga => ga != null && ga.Code == code)
                .FirstOrDefault();
            return general != null
                       ? (GeneralAccountDto)new GeneralAccountDto().InjectFrom<UnflatLoopValueInjection>(general)
                       : null;
        }


        public string AddIndexAccount(IndexAccountDto indexAccountDto)
        {
            try
            {
                IndexAccountRepository repository = new IndexAccountRepository();
                GeneralAccount generalAccount =
                    repository.ActiveContext.GeneralAccounts.Include("IndexAccounts")
                              .FirstOrDefault(ga => ga.Id == indexAccountDto.GeneralAccountId);
                if (!generalAccount.ContainIndexAccount(code: indexAccountDto.Code))
                {
                    IndexAccount indexAccount = new IndexAccount();
                    indexAccountDto.Id = Guid.NewGuid();
                    indexAccount.InjectFrom<UnflatLoopValueInjection>(indexAccountDto);
                    indexAccount.GeneralAccount = generalAccount;
                    repository.Add(indexAccount);
                    return "has successfully created";
                }
                return "this record with this code was there in database";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public string RemoveIndexAccount(Guid id)
        {
            try
            {
                IndexAccountRepository repository = new IndexAccountRepository();
                IndexAccount indexAccount =
                    repository.ActiveContext.IndexAccounts.Include("Accounts").Where(ia => ia.Id == id).FirstOrDefault();
                if (indexAccount == null) return "no record was found by this id";
                if (indexAccount.ContainAccounts) return "this record has account record and can not be deleted";
                repository.Remove(indexAccount);
                return "has successfully created";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public IndexAccountDto GetIndexAccount(string code)
        {
            IndexAccountRepository repository = new IndexAccountRepository();
            IndexAccount indexAccount = repository.ActiveContext.IndexAccounts
                .Where(ia => ia != null && ia.Code == code)
                .FirstOrDefault();

            if (indexAccount != null)
            {
                IndexAccountDto indexAccountDto = (IndexAccountDto)new IndexAccountDto().InjectFrom<UnflatLoopValueInjection>(indexAccount);
                if (indexAccount.GeneralAccount != null) indexAccountDto.GeneralAccountId = indexAccount.GeneralAccount.Id;
                return indexAccountDto;                
            }
            return null;
        }

        public string AddAccount(AccountDto accountDto)
        {
            try
            {
                AccountRepository repository = new AccountRepository();
                IndexAccount indexAccount =
                    repository.ActiveContext.IndexAccounts.Include("BankAccounts")
                              .FirstOrDefault(ia => ia.Id == accountDto.IndexAccountId);
                if (!indexAccount.ContainAccount(accountDto.Code))
                {
                    Account account = new Account();
                    accountDto.Id = Guid.NewGuid();
                    account.InjectFrom<UnflatLoopValueInjection>(accountDto);
                    account.IndexAccount = indexAccount;
                    repository.Add(account);
                    return "account is created";
                }
                return "account with this code is recreated !?";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }


        public AccountDto GetBankAccount(string code)
        {
            AccountRepository repository = new AccountRepository();
            BankAccount bankAccount = repository.ActiveContext.BankAccounts
                .Where(a => a != null && a.Code == code)
                .FirstOrDefault();
            if (bankAccount != null)
            {
                AccountDto accountDto = (AccountDto)new AccountDto().InjectFrom(bankAccount);
                accountDto.IndexAccountId = bankAccount.IndexAccount.Id;
                return accountDto;
            }
            return null;
        }


        public AccountDto GetAccount(string code)
        {
            AccountRepository repository = new AccountRepository();
            Account account = repository.ActiveContext.BankAccounts.OfType<Account>().Where(a => a != null && a.Code == code)
                .FirstOrDefault();

            if (account != null)
            {
                AccountDto accountDto = (AccountDto)new AccountDto().InjectFrom(account);
                accountDto.IndexAccountId = account.IndexAccount.Id;
                return accountDto;
            }
            return null;
        }

        public SimpleAccountDto GetSimpleBankAccount(string code)
        {
            AccountRepository repository = new AccountRepository();
            BankAccount bankAccount = repository.ActiveContext.BankAccounts
                .Where(a => a != null && a.Code == code)
                .FirstOrDefault();
            if (bankAccount != null)
            {
                SimpleAccountDto simpleAccountDto = (SimpleAccountDto)new SimpleAccountDto().InjectFrom(bankAccount);
                simpleAccountDto.IndexAccountId = bankAccount.IndexAccount.Id;
                return simpleAccountDto;
            }
            return null;
        }

        public SimpleAccountDto GetSimpleAccount(string code)
        {
            AccountRepository repository = new AccountRepository();
            BankAccount account = repository.ActiveContext.BankAccounts.OfType<Account>()
                .Where(a => a.Code == code)
                .FirstOrDefault();
            if (account != null)
            {
                SimpleAccountDto simpleAccountDto = (SimpleAccountDto)new SimpleAccountDto().InjectFrom(account);
                simpleAccountDto.IndexAccountId = account.IndexAccount.Id;
                return simpleAccountDto;
            }
            return null;

        }

        public SimpleAccountDto GetSimpleLoanAccount(string code)
        {
            AccountRepository repository = new AccountRepository();
            BankAccount account = repository.ActiveContext.BankAccounts.OfType<Loan>().FirstOrDefault(a => a.Code == code);
            if (account == null)
            {
                return null;
            }
            SimpleAccountDto simpleAccountDto = (SimpleAccountDto)new SimpleAccountDto().InjectFrom(account);
            simpleAccountDto.IndexAccountId = account.IndexAccount.Id;
            return simpleAccountDto;
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


//        private AccountDto TranslateAccountToAccountDto(Account account)
//        {
//            if (account == null)
//            {
//                AccountDto nullAccountDto = new AccountDto { Description = "record not found by this code" };
//                return nullAccountDto;
//            }
//            AccountDto bankAccountDto = new AccountDto()
//                {
//                    Id = account.Id,
//                    Code = account.Code,
//                    Description = account.Description,
//                    IndexAccountCode = account.IndexAccountCode,
//                    No = account.No,
//                    //                RowId = account.RowId,
//                    Balance = account.Balance,
//                };
//            return bankAccountDto;
//        }



 /*public bool Bardasht(string account, double amount)
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
//        private IndexAccountDto TranslateIndexAccountToIndexAccountDto(IndexAccount indexAccount)
//        {
//            if (indexAccount == null)
//            {
//                IndexAccountDto nullIndexAccountDto = new IndexAccountDto
//                    {
//                        Description = "record not found by this code"
//                    };
//                return nullIndexAccountDto;
//            }
//            IndexAccountDto indexAccountDto = new IndexAccountDto()
//                {
//                    Id = indexAccount.Id,
//                    Code = indexAccount.Code,
//                    Description = indexAccount.Description,
//                    GeneralAccountId = indexAccount.GeneralAccount.Id,
//                    GeneralAccountCode = indexAccount.GeneralAccountCode,
//                    RowId = indexAccount.RowId,
//                    HaveAccounts = indexAccount.HaveAccounts,
//                };
//            return indexAccountDto;
//        }

//                var personDtos1 = account.Customers.Select(
//                    c =>
//                    new PersonDto
//                        {
//                            Id = c.Person.Id,
//                            BirthDate = c.Person.BirthDate,
//                            CretyId = c.Person.CretyId,
//                            CretySerial = c.Person.CretySerial,
//                            CustomerId = c.Person.CustomerId,
//                            FatherName = c.Person.FatherName,
//                            Firstname = c.Person.Firstname,
//                            HeadNationalIdentity = c.Person.HeadNationalIdentity,
//                            HomeAddress = c.Person.ContactInfo.HomeAddress,
//                            HomeTelno = c.Person.ContactInfo.HomeTelno,
//                            IndivOrOrgan = c.Person.IndivOrOrgan,
//                            JobKind = c.Person.JobKind,
//                            JobName = c.Person.JobName,
//                            NationalIdentity = c.Person.NationaliIdentity,
//                            Lastname = c.Person.Lastname,
//                            Mobile = c.Person.ContactInfo.Mobile,
//                            OfficeTelNo = c.Person.ContactInfo.OfficeTelNo,
//                            PostalIdentity = c.Person.ContactInfo.PostalIdentity,
//                            Sadereh = c.Person.Sadereh,
//                            Salary = c.Person.Salary,
//                            ShobehCode = c.Person.ShobehCode,
//                            WorkAddress = c.Person.ContactInfo.WorkAddress
//                        })
//                    .ToList()
//                                        .Union(
//                                            account.Lawyers.Select(
//                                                l =>
//                                                new PersonDto
//                                                    {
//                                                        Id = l.Person.Id,
//                                                        BirthDate = l.Person.BirthDate,
//                                                        CretyId = l.Person.CretyId,
//                                                        CretySerial = l.Person.CretySerial,
//                                                        CustomerId = l.Person.CustomerId,
//                                                        FatherName = l.Person.FatherName,
//                                                        Firstname = l.Person.Firstname,
//                                                        HeadNationalIdentity = l.Person.HeadNationalIdentity,
//                                                        HomeAddress = l.Person.ContactInfo.HomeAddress,
//                                                        HomeTelno = l.Person.ContactInfo.HomeTelno,
//                                                        IndivOrOrgan = l.Person.IndivOrOrgan,
//                                                        JobKind = l.Person.JobKind,
//                                                        JobName = l.Person.JobName,
//                                                        NationalIdentity = l.Person.NationaliIdentity,
//                                                        Lastname = l.Person.Lastname,
//                                                        Mobile = l.Person.ContactInfo.Mobile,
//                                                        OfficeTelNo = l.Person.ContactInfo.OfficeTelNo,
//                                                        PostalIdentity = l.Person.ContactInfo.PostalIdentity,
//                                                        Sadereh = l.Person.Sadereh,
//                                                        Salary = l.Person.Salary,
//                                                        ShobehCode = l.Person.ShobehCode,
//                                                        WorkAddress = l.Person.ContactInfo.WorkAddress
//                                                    })
//                           .ToList());
//
//        private PersonDto TranslatePersonToPersonDto(Person person)
//        {
//            if (person == null)
//            {
//                PersonDto nullPesonDto = new PersonDto { Firstname = "record not found by this national identity" };
//                return nullPesonDto;
//            }
//            PersonDto personDto = new PersonDto
//                {
//                    Id = person.Id,
//                    CustomerId = person.CustomerId,
//                    ShobehCode = person.ShobehCode,
//                    Firstname = person.Firstname,
//                    Lastname = person.Lastname,
//                    FatherName = person.FatherName,
//                    CretyId = person.CretyId,
//                    CretySerial = person.CretySerial,
//                    Sadereh = person.Sadereh,
//                    BirthDate = person.BirthDate,
//                    NationalIdentity = person.NationalIdentity,
//                    HeadNationalIdentity = person.HeadNationalIdentity,
//                    JobName = person.JobName,
//                    JobKind = person.JobKind,
//                    Salary = person.Salary,
//                    IndivOrOrgan = person.IndivOrOrgan,
//                    HomeAddress = person.ContactInfo.HomeAddress,
//                    WorkAddress = person.ContactInfo.WorkAddress,
//                    HomeTelno = person.ContactInfo.HomeTelno,
//                    OfficeTelNo = person.ContactInfo.OfficeTelNo,
//                    Mobile = person.ContactInfo.Mobile,
//                    PostalIdentity = person.ContactInfo.PostalIdentity,
//                };
//            return personDto;
//        }
//
//
//        private void TranslatePersonDtoToPerson(PersonDto personDto, Person person)
//        {
//            person.CustomerId = personDto.CustomerId;
//            person.ShobehCode = personDto.ShobehCode;
//            person.Firstname = personDto.Firstname;
//            person.Lastname = personDto.Lastname;
//            person.FatherName = personDto.FatherName;
//            person.CretyId = personDto.CretyId;
//            person.CretySerial = personDto.CretySerial;
//            person.Sadereh = personDto.Sadereh;
//            person.NationalIdentity = personDto.NationalIdentity;
//            person.HeadNationalIdentity = personDto.HeadNationalIdentity;
//            person.JobName = personDto.JobName;
//            person.JobKind = personDto.JobKind;
//            person.Salary = personDto.Salary;
//            person.ContactInfo.HomeAddress = personDto.HomeAddress;
//            person.ContactInfo.WorkAddress = personDto.WorkAddress;
//            person.ContactInfo.HomeTelno = personDto.HomeTelno;
//            person.ContactInfo.OfficeTelNo = personDto.OfficeTelNo;
//            person.ContactInfo.Mobile = personDto.Mobile;
//            person.ContactInfo.PostalIdentity = personDto.PostalIdentity;
//        }

//        private GeneralAccountDto TranslateGeneralAccountToGeneralAccountDto(GeneralAccount generalAccount)
//        {
//            if (generalAccount == null)
//            {
//                GeneralAccountDto nullGeneralAccountDto = new GeneralAccountDto
//                    {
//                        Description = "record not found by this code"
//                    };
//                return nullGeneralAccountDto;
//            }
//            GeneralAccountDto generalAccountDto = new GeneralAccountDto
//                {
//                    Id = generalAccount.Id,
//                    Code = generalAccount.Code,
//                    Description = generalAccount.Description,
//                    Category = generalAccount.Category,
//                };
//            return generalAccountDto;
//        }

#endregion
