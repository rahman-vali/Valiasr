using System;
using System.Linq;

namespace Valiasr.Service
{
    using System.Collections.Generic;
    using System.Data.Entity;

    using Valiasr.DataAccess.Repositories;
    using Valiasr.Domain.Model;

    using Xunit;
    using Xunit.Extensions;

    public class PersonAccountService : IPersonAccountService
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
                if (repository.ActiveContext.Persons.Where(p => p.NationaliIdentity == nationalIdentity).Count() != 0) return "person with this natinal identity is there ";
                personDto.CustomerId =
                    repository.ActiveContext.Persons.Select(o => o.CustomerId).DefaultIfEmpty(0).Max() + 1;
                Person person = new Person();
                TranslatePersonDtoToPerson(personDto, person);
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
                TranslatePersonDtoToPerson(personDto, person);
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
            Person person = repository.GetPersonByNationalIdentity(nationalIdentity);
            return TranslatePersonToPersonDto(person);
        }


        public List<PersonDto> GetPersonByAccount(string code)
        {
            PersonRepository repository = new PersonRepository();
            Account account =
                repository.ActiveContext.BankAccounts.OfType<Account>()
                          .Include("Customers.Person")
                          .Include("Lawyers.Person")
                          .FirstOrDefault(a => a.Code == code);

            if (account != null)
            {
                var persons =
                    account.Customers.Select(c => c.Person)
                           .ToList()
                           .Union(account.Lawyers.Select(l => l.Person))
                           .ToList();
                if (persons.Count == 0) return null;
                List<PersonDto> personDtos = new List<PersonDto>();
                foreach (Person person in persons)
                {
                    personDtos.Add(this.TranslatePersonToPersonDto(person));
                }
                return personDtos;
            }
            return null;
        }


        public PersonDto GetPersonById(Guid id)
        {
            PersonRepository repository = new PersonRepository();
            Person person = repository.ActiveContext.Persons.Where(p => p.Id == id).FirstOrDefault();
            return TranslatePersonToPersonDto(person);
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

        private PersonDto TranslatePersonToPersonDto(Person person)
        {
            if (person == null)
            {
                PersonDto nullPesonDto = new PersonDto { Firstname = "record not found by this national identity" };
                return nullPesonDto;
            }
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

        public string AddGeneralAccount(GeneralAccountDto generalAccountDto)
        {
            try
            {
                int code = generalAccountDto.Code;
                GeneralAccountRepository repository = new GeneralAccountRepository();
                if ((repository.ActiveContext.GeneralAccounts.Count(ga => ga.Code == code)) == 0)
                {
                    GeneralAccount generalAccount = GeneralAccount.CreateGeneralAccount(
                        generalAccountDto.Code, generalAccountDto.Description, generalAccountDto.Category);
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
            GeneralAccount generalAccount = new GeneralAccount();
            return TranslateGeneralAccountToGeneralAccountDto(generalAccount);
        }

        private GeneralAccountDto TranslateGeneralAccountToGeneralAccountDto(GeneralAccount generalAccount)
        {
            if (generalAccount == null)
            {
                GeneralAccountDto nullGeneralAccountDto = new GeneralAccountDto
                    {
                        Description = "record not found by this code"
                    };
                return nullGeneralAccountDto;
            }
            GeneralAccountDto generalAccountDto = new GeneralAccountDto
                {
                    Id = generalAccount.Id,
                    Code = generalAccount.Code,
                    Description = generalAccount.Description,
                    Category = generalAccount.Category,
                };
            return generalAccountDto;
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
                    IndexAccount indexAccount = IndexAccount.CreateIndexAccount(
                        generalAccount,
                        indexAccountDto.Code,
                        indexAccountDto.GeneralAccountCode,
                        indexAccountDto.RowId,
                        indexAccountDto.Description,
                        indexAccountDto.HaveAccounts);
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
            IndexAccount indexAccount =
                repository.ActiveContext.IndexAccounts.Where(ia => ia.Code == code).FirstOrDefault();
            return TranslateIndexAccountToIndexAccountDto(indexAccount);
        }

        private IndexAccountDto TranslateIndexAccountToIndexAccountDto(IndexAccount indexAccount)
        {
            if (indexAccount == null)
            {
                IndexAccountDto nullIndexAccountDto = new IndexAccountDto
                    {
                        Description = "record not found by this code"
                    };
                return nullIndexAccountDto;
            }
            IndexAccountDto indexAccountDto = new IndexAccountDto()
                {
                    Id = indexAccount.Id,
                    Code = indexAccount.Code,
                    Description = indexAccount.Description,
                    GeneralAccountId = indexAccount.GeneralAccount.Id,
                    GeneralAccountCode = indexAccount.GeneralAccountCode,
                    RowId = indexAccount.RowId,
                    HaveAccounts = indexAccount.HaveAccounts,
                };
            return indexAccountDto;
        }

        public string AddAccount(AccountDto accountDto)
        {
            try
            {
                AccountRepository repository = new AccountRepository();
                IndexAccount indexAccount =
                    repository.ActiveContext.IndexAccounts.Include("Accounts")
                              .FirstOrDefault(ia => ia.Id == accountDto.IndexAccountId);
                if (!indexAccount.ContainAccount(accountDto.Code))
                {
                    Account account = Account.CreateAccount(
                        indexAccount,
                        accountDto.Code,
                        accountDto.No,
                        accountDto.RowId,
                        accountDto.Balance,
                        accountDto.Description);
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


        public AccountDto GetAccount(string code)
        {
            AccountRepository repository = new AccountRepository();
            Account account =
                repository.ActiveContext.BankAccounts.OfType<Account>().Where(a => a.Code == code).FirstOrDefault();
            return this.TranslateAccountToAccountDto(account);
        }

        private AccountDto TranslateAccountToAccountDto(Account account)
        {
            if (account == null)
            {
                AccountDto nullAccountDto = new AccountDto { Description = "record not found by this code" };
                return nullAccountDto;
            }
            AccountDto indexAccountDto = new AccountDto()
                {
                    Id = account.Id,
                    Code = account.Code,
                    Description = account.Description,
                    IndexAccountCode = account.IndexAccountCode,
                    No = account.No,
                    //                RowId = account.RowId,
                    Balance = account.Balance,
                };
            return indexAccountDto;
        }


        public string AddLoanRequest(LoanRequestDto loanRequestDto)
        {
            try
            {
                LoanRequestRepository repository = new LoanRequestRepository();
                loanRequestDto.ReqNo = MakeReqNo(repository, loanRequestDto.Date);
                LoanRequest loanRequest = new LoanRequest();
                Account account =
                    repository.ActiveContext.BankAccounts.OfType<Account>()
                              .FirstOrDefault(a => a.Code == loanRequestDto.AccountCode);
                TranslateLoanRequestDtoToLoanRequest(loanRequestDto, loanRequest);
                loanRequest.Account = account;
                repository.Add(loanRequest);
                return "request added successfully";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        private void TranslateLoanRequestDtoToLoanRequest(LoanRequestDto loanRequestDto, LoanRequest loanRequest)
        {
            loanRequest.ReqNo = loanRequestDto.ReqNo;
            loanRequest.LoanRequestDate = loanRequestDto.Date;
            loanRequest.Amount = loanRequestDto.Amount;
            loanRequest.AccountCode = loanRequestDto.AccountCode;
            loanRequest.Description = loanRequestDto.Description;
            loanRequest.Duration = loanRequestDto.Duration;
            loanRequest.DurationType = loanRequestDto.DurationType;
            loanRequest.PaymentCount = loanRequestDto.PaymentCount;
            loanRequest.LastDate = loanRequestDto.LastDate;
            loanRequest.RequestKind = loanRequestDto.RequestKind;
            loanRequest.IndivOrOrgan = loanRequestDto.IndivOrOrgan;
            loanRequest.FingerRegId = loanRequestDto.FingerRegId;
        }

        private int MakeReqNo(LoanRequestRepository repository, int date)
        {
            int no =
                repository.ActiveContext.LoanRequests.Where(lr => lr.LoanRequestDate == date)
                          .Select(lr => lr.ReqNo)
                          .DefaultIfEmpty(0)
                          .Max() + 1;
            if (no == 1) no = date * 1000 + 1;
            return no;
        }

        public LoanRequestDto GetLoanRequest(int reqNo)
        {
            LoanRequestRepository repository = new LoanRequestRepository();
            LoanRequest loanRequest = repository.ActiveContext.LoanRequests.FirstOrDefault(lr => lr.ReqNo == reqNo);
            return TranslateLoanRequestToLoanRequestDto(loanRequest);

        }

        private LoanRequestDto TranslateLoanRequestToLoanRequestDto(LoanRequest loanRequest)
        {
            if (loanRequest == null)
            {
                LoanRequestDto loanRequestDtoNull = new LoanRequestDto { Description = "record not found" };
                return loanRequestDtoNull;
            }
            LoanRequestDto loanRequestDto = new LoanRequestDto
                {
                    ReqNo = loanRequest.ReqNo,
                    AccountCode = loanRequest.AccountCode,
                    Amount = loanRequest.Amount,
                    Date = loanRequest.LoanRequestDate,
                    Description = loanRequest.Description,
                    Duration = loanRequest.Duration,
                    DurationType = loanRequest.DurationType,
                    FingerRegId = loanRequest.FingerRegId,
                    Id = loanRequest.Id,
                    IndivOrOrgan = loanRequest.IndivOrOrgan,
                };
            return loanRequestDto;
        }

        public string UpdaeLoanRequest(LoanRequestDto loanRequestDto)
        {
            try
            {
                Guid id = loanRequestDto.Id;
                LoanRequestRepository repository = new LoanRequestRepository();
                LoanRequest loanRequest =
                    repository.ActiveContext.LoanRequests.Where(lr => lr.Id == id).FirstOrDefault();
                if (loanRequest == null) return "request is not there ";
                this.TranslateLoanRequestDtoToLoanRequest(loanRequestDto, loanRequest);
                repository.Update(loanRequest);
                return "updated successfully";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public string RemoveLoanRequest(int reqNo)
        {
            LoanRequestRepository repository = new LoanRequestRepository();
            LoanRequest loanRequest = repository.ActiveContext.LoanRequests.FirstOrDefault(lr => lr.ReqNo == reqNo);
            if (loanRequest == null) return "record not found to be deleted";

            return "";
        }

        public string AddOrUpdateLoanRequestOkyAssistant(Guid loanRequestId, LoanRequestOkyDto loanRequestOkyDto)
        {
            try
            {
                LoanRequestRepository repository = new LoanRequestRepository();
                LoanRequest loanRequest =
                    repository.ActiveContext.LoanRequests.FirstOrDefault(lr => lr.Id == loanRequestId);
                if (loanRequest == null) return "this request with this Request no :" + loanRequestOkyDto.ReqNo.ToString() + " is not there";
                TranslateLoanRequestOkyDto(loanRequestOkyDto, loanRequest);
                repository.Update(loanRequest);
                return "requestOkyAss added successfully";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }


        private void TranslateLoanRequestOkyDto(LoanRequestOkyDto loanRequestOkyDto, LoanRequest loanRequest)
        {
            loanRequest.LoanRequestOkyAsistant.OKyDate = loanRequestOkyDto.OKyDate;
            loanRequest.LoanRequestOkyAsistant.OkyAss = loanRequestOkyDto.OkyAss;
            loanRequest.LoanRequestOkyAsistant.OkyQty = loanRequestOkyDto.OkyQty;
            loanRequest.LoanRequestOkyAsistant.PaymentCount = loanRequestOkyDto.PaymentCount;
            loanRequest.LoanRequestOkyAsistant.OkyDuration = loanRequestOkyDto.OkyDuration;
            loanRequest.LoanRequestOkyAsistant.OkyDurationType = loanRequestOkyDto.OkyDurationType;
            loanRequest.LoanRequestOkyAsistant.RegPerId = loanRequestOkyDto.RegPerId;
        }

        public string AddRequestAccountAve(Guid loanRequestId, RequestAccountAveDto requestAccountAveDto)
        {
            try
            {
                string accountCode = requestAccountAveDto.AccountCode;
                LoanRequestRepository repository = new LoanRequestRepository();
                LoanRequest loanRequest = repository.ActiveContext.LoanRequests.FirstOrDefault(lr => lr.Id == loanRequestId);
                if (loanRequest == null) return "this request with this Request no :" + requestAccountAveDto.ReqNo.ToString() + " is not there";
                Account account = repository.ActiveContext.BankAccounts.OfType<Account>().FirstOrDefault(a => a.Code == accountCode);
                if (account == null) return "account is not found";
                RequestAccountAve requestAccountAve = new RequestAccountAve();
                this.TranslateRequestAccountAveDtoToRequestAccountAve(requestAccountAveDto,requestAccountAve);
                requestAccountAve.Account = account;
                loanRequest.RequestAccountAves.Add(requestAccountAve);
                repository.ActiveContext.SaveChanges();
                return "record added successfully";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        private void TranslateRequestAccountAveDtoToRequestAccountAve(RequestAccountAveDto requestAccountAveDto, RequestAccountAve requestAccountAve)
        {

            requestAccountAve.ReqNo = requestAccountAveDto.ReqNo;
            requestAccountAve.Average = requestAccountAveDto.Average;
            requestAccountAve.fromDate = requestAccountAveDto.fromDate;
            requestAccountAve.ToDate = requestAccountAveDto.ToDate;
            requestAccountAve.AccountCode = requestAccountAveDto.AccountCode;
            requestAccountAve.DebtQty = requestAccountAveDto.DebtQty;
            requestAccountAve.ConsumedQty = requestAccountAveDto.ConsumedQty;
            requestAccountAve.LastBalance = requestAccountAveDto.LastBalance;
            requestAccountAve.LastDate = requestAccountAveDto.LastDate;
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

#endregion
