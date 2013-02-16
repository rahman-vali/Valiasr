
namespace Valiasr.Service.Test
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Valiasr.DataAccess;
    using Valiasr.DataAccess.Repositories;
    using Valiasr.Domain.Model;
    using Valiasr.Service;

    using Xunit;
    using Xunit.Extensions;

    public class SrviceTest
    {

        [Fact]
        public int AddPersonTest2()
        {
            PersonAccountService pa = new PersonAccountService();
            PersonDto personDto = new PersonDto
                {
                    BirthDate = 13910102,
                    CretyId = "1",
                    CretySerial = "2",
                    FatherName = "ahmad",
                    Firstname = "ali",
                    HomeAddress = "babol",
                    NationalIdentity = "1",
                    HomeTelno = "1245",
                    HeadNationalIdentity = "2"
                };
            pa.AddPerson(personDto);
            return 100;

        }

        public static IEnumerable<object[]> TesPersontData { get { yield return new object[] { Guid.Parse("be74180c-3cb0-4ac9-860f-16a39a64ab69") }; } }

          [Theory]
          [PropertyData("TestPersonData")]
          public string RemovePerson(Guid id)
          {
              
                  if (id == Guid.Empty) return ("person's id is not vali and can not be deleted");
                  PersonRepository repository = new PersonRepository();
                  Person person = repository.ActiveContext.Persons.Where(p => p.Id == id).FirstOrDefault();
                  var accounts = repository.ActiveContext.BankAccounts.OfType<Account>().Include("Customers")
                            .Include("Lawyers")
                            .Where(
                                o => o.Customers.Any(c => c.Person.Id == id) || o.Lawyers.Any(l => l.Person.Id == id)).ToList();
                  var result = accounts.SelectMany(o => o.Customers).Where(o => o.Person.Id == id);

                  if (result.Any())
                  {
                      return "This person is customer in these/this accounts: " + String.Join(", ", result.Select(o => o.No).ToArray());
                  }
                  if (person == null) return ("person not found and can not be updated");
                  repository.Remove(person);
                  return ("person successfully removed");              
          }
    [Fact]
        public object getPersonByNationalIdentity()
        {
            PersonAccountService personAccountService = new PersonAccountService();
            PersonDto personDto = personAccountService.GetPersonByNationalIdentity("1");
            return null;
        }



    public static IEnumerable<object[]> TestAccountData { get { yield return new object[] { Guid.Parse("50ee97cc-411a-4944-994b-e2199298adca"), Guid.Parse("f1e93aef-6db4-4e53-bc59-19510800f3b1") }; } }

          [Theory]
          [PropertyData("TestAccountData")]
          public string AddCustomerToAccount(Guid accountId, Guid id)
          {
              CustomerDto customerDto = new CustomerDto();
              customerDto.PersonId = id;
              customerDto.No = "4";
              customerDto.Portion = 1;
              PersonAccountService personAccountService = new PersonAccountService();
              personAccountService.AddCustomerToAccount(accountId, customerDto);
              return "completed";
          }
        [Fact]
        public object GetPersonByAccountTest()
        {
//            AccountRepository repository = new AccountRepository();
//            Account account = repository.ActiveContext.Accounts.Include("Lawyers.Person").Include("Customers.Person").FirstOrDefault(a => a.Code == "1/0/0");
//            var firstnames = account.Lawyers.Select(o => o.Person.Firstname).Union(account.Customers.Select(a => a.Person.Firstname)).ToList();
            PersonAccountService personAccountService = new PersonAccountService();
            List<PersonDto> list = personAccountService.GetPersonByAccount("1/0/0");
            
return null;

        }
          [Fact]
          public void AddGeneralAccountTest()
          {
              
                  GeneralAccountDto generalAccountDto = new GeneralAccountDto();
                  generalAccountDto.Category = 1;
                  generalAccountDto.Code = 7;
                  generalAccountDto.Description = "bank";
                  PersonAccountService pa = new PersonAccountService();
              pa.AddGeneralAccount(generalAccountDto);
           //   GeneralAccountRepository repository = new GeneralAccountRepository();
             //      Assert.True(repository.ActiveContext.GeneralAccounts.Where(ga => ga.Code == 15).Count() == 1);

          }
          public static IEnumerable<object[]> TestIndexAccountData { get { yield return new object[] { Guid.Parse("e315dc2f-f4bf-4433-ab1b-927b2829224f") }; } }

          [Theory]
          [PropertyData("TestIndexAccountData")]
          public void AddIndexAccountTest(Guid id)
          {
              
                  IndexAccountDto indexAccountDto = new IndexAccountDto();
                  indexAccountDto.Code = "2/0";
                  indexAccountDto.Description = "Bank mell";
                  indexAccountDto.HaveAccounts = true;
                  indexAccountDto.GeneralAccountCode = 4;
                  indexAccountDto.RowId = 0;
                  indexAccountDto.GeneralAccountId = id;
                  PersonAccountService pa = new PersonAccountService();
                  string str = pa.AddIndexAccount(indexAccountDto); 
              
          }



        public static IEnumerable<object[]> TestPersonData { get { yield return new object[] { Guid.Parse("41890589-AB4C-4BDD-A4D9-08A0EB69EC09") }; } }

        [Theory]
          [PropertyData("TestPersonData")]
          public void AddPersonTest1(Guid id)
          {
              PersonDto personDto = new PersonDto();
              personDto.HeadNationalIdentity = "1245";
              personDto.Id = id;
              personDto.CustomerId = 10;
              personDto.Firstname = "ali";
              personDto.FatherName = "ahmad";
              personDto.Lastname = "rezai";
              personDto.ShobehCode = 1;
              personDto.HomeAddress = "babol";
              personDto.HomeTelno = "145789+";
              personDto.NationalIdentity = "198598";
              PersonAccountService pa = new PersonAccountService();
              string str = pa.AddPerson(personDto);
          }
        [Fact]
        public void AddPersonTest()
        {
            PersonDto personDto = new PersonDto();
            personDto.HeadNationalIdentity = "12451";
            //personDto.Id = Guid.NewGuid();
           // personDto.CustomerId = 10;
            personDto.Firstname = "ali1";
            personDto.FatherName = "ahmad1";
            personDto.Lastname = "rezai1";
            personDto.ShobehCode = 11;
            personDto.HomeAddress = "babol1";
            personDto.HomeTelno = "145789+";
            personDto.NationalIdentity = "56457611";
            PersonAccountService pa = new PersonAccountService();
            string str = pa.AddPerson(personDto);
        }

        public static IEnumerable<object[]> TestRemovePerson { get { yield return new object[] { Guid.Parse("6e65779a-0fc5-45a3-ae9d-2dc382634f7d") }; } }

        [Theory]
        [PropertyData("TestRemovePerson")]
        public void removePerson(Guid id)
        {
            PersonAccountService personAccountService = new PersonAccountService();
            string str = personAccountService.RemovePerson(id);
        }

        public static IEnumerable<object[]> TestAddAccountData { get { yield return new object[] { Guid.Parse("be291285-c79f-4741-84fc-4bd6fea24b04") }; } }

          [Theory]
          [PropertyData("TestAddAccountData")]
          public void AddAccountTest(Guid id)
          {
              
                  AccountDto accountDto = new AccountDto();
                  accountDto.Code = "2/0/0";
                  accountDto.Balance = 10000;
                  accountDto.Description = "bank melli 213";
                  accountDto.IndexAccountId = id;
                  accountDto.IndexAccountCode = "2/0";
                  accountDto.RowId = 0;
                  accountDto.No = "120000";
                  PersonAccountService pa = new PersonAccountService();
                  string str = pa.AddAccount(accountDto);
              
          }
          [Fact]
          public void GetPersonTest()
          {
              PersonAccountService pa = new PersonAccountService();
              PersonDto personDto = pa.GetPersonByNationalIdentity("198598");
              var a = personDto.NationalIdentity;

          }
          [Fact]
          public void GetGeneraAccountTest()
          {
              PersonAccountService pa = new PersonAccountService();
              GeneralAccountDto generalAccountDto = pa.GetGeneralAccount(1);
              var a = generalAccountDto.Description;
          }

        [Fact]
        public void GetIndexAccountTest()
        {
            PersonAccountService pa = new PersonAccountService();
            IndexAccountDto indexAccountDto = pa.GetIndexAccount("1/0");
            var a = indexAccountDto.Description;
        }

        [Fact]
        public void GetAccountTest()
        {
            PersonAccountService ps = new PersonAccountService();
            AccountDto accountDto = ps.GetAccount("1/0/0");
            var a = accountDto.RowId;
        }
        [Fact]
        public void Creat_Request()
        {

            LoanRequestDto loanRequestDto = new LoanRequestDto {Id = Guid.NewGuid() , AccountCode = "1/0/0",Amount = 1000,Date = 13911102,Description = "vam" };
            PersonAccountService ps = new PersonAccountService();
            string str = ps.AddLoanRequest(loanRequestDto);
        }

        public static IEnumerable<object[]> RequestOky { get { yield return new object[] { Guid.Parse("{011142f3-4ec3-4c76-a840-b05d1d84fff0") }; } }

        [Theory]
        [PropertyData("RequestOky")]

        public void Creat_RequestOkyAss(Guid id)
        {
            PersonAccountService ps = new PersonAccountService();
            LoanRequestOkyDto loanRequestOkyDto = new LoanRequestOkyDto();
            loanRequestOkyDto.ReqNo = 1;
            loanRequestOkyDto.OKyDate = 13911032;
            loanRequestOkyDto.OkyAss = "moavenreis";
            string str = ps.AddOrUpdateLoanRequestOkyAssistant(id ,loanRequestOkyDto);
        }

        public static IEnumerable<object[]> RequestAve { get { yield return new object[] { Guid.Parse("2fad0e21-f8cb-4a25-a622-5a330b3e396d") }; } }

        [Theory]
        [PropertyData("RequestAve")]

        public void Creat_RequestAve(Guid id)
        {
            PersonAccountService ps = new PersonAccountService();
            RequestAccountAveDto requestAccountAveDto = new RequestAccountAveDto{AccountCode = "2/0/0" ,AverageId = 1 ,AverageQty = 90 ,ConsumedQty = 80,DebtQty = 70,fromDate = 911008,LastBalance = 68,LastDate = 0,ReqNo = 1,Id = Guid.NewGuid()};
            string str = ps.AddRequestAccountAve(id, requestAccountAveDto);
        }



        [Fact]
        public void Creat_Loan()
        {
            var context = new ValiasrContext("Valiasr");
            var loanRequest = context.LoanRequests.Where(a => a.ReqNo == 1).FirstOrDefault();
            var account = context.BankAccounts.Where(a => a.Code == "1/0/0").FirstOrDefault();
            //var loan = new Loan { Id = Guid.NewGuid(), Amount = 2000 ,LoanRequest = loanRequest,Account = account};
            //loanRequest.Loan = loan;
            // context.LoanRequests.Add(loanRequest);
            //account.Loan = loan;
            context.SaveChanges();

        }


        public static IEnumerable<object[]> CustomerLawyerTest { get { yield return new object[] { Guid.Parse("1528fc7d-136b-4ac2-8ba1-0303a6c30560") }; } }

        [Theory]
        [PropertyData("CustomerLawyerTest")]
        private bool PersonIsCustomerOrLawyer( Guid id)
        {
            string messageStr = "";
            PersonRepository repository = new PersonRepository();
            var customerAccounts =
                repository.ActiveContext.BankAccounts.OfType<Account>().Include("Customers")
                          .Where(a => a.Customers.Any(c => c.Person.Id == id))
                          .ToList();
            if (customerAccounts.Any())
            {
                messageStr = "this person is customer in these/this accounts :  " +
                             string.Join(",", customerAccounts.Select(ca => ca.Code).ToArray());
            }
            var lawyerAccounts =
                repository.ActiveContext.BankAccounts.OfType<Account>().Include("Lawyers")
                          .Where(a => a.Customers.Any(c => c.Person.Id == id))
                          .ToList();
            if (lawyerAccounts.Any())
            {
                messageStr = messageStr = string.Format("{0} And lawyer in these/this accounts:  {1}", messageStr, string.Join(",", lawyerAccounts.Select(ca => ca.Code).ToArray()));
            }
            return messageStr.Length != 0;
        }

        public LoanRequest Create_Request(Account account)
        {

            var LoanRequest = new LoanRequest
            {
                Id = Guid.NewGuid(),
                Account = account,
                Amount = 5000,
                ReqNo = 1,
                Description = "h",
                Duration = 20,
                DurationType = "kl",
                FingerRegId = 4,
                LastDate = 13921024,
            };
            LoanRequestOkyAssistant loanRequestOkyAsistant = new LoanRequestOkyAssistant();           
            LoanRequest.LoanRequestOkyAsistant = loanRequestOkyAsistant;
            return LoanRequest;
        }

        [Fact]
        public decimal GetAve()
        {
            AccountRepository repository = new AccountRepository();
            Account account =
                repository.ActiveContext.BankAccounts.OfType<Account>().FirstOrDefault(a => a.Code == "1/0/0");
            decimal untilDateBalance  = 0;
            decimal untilDateBedehkar = 0 ;
            decimal emtiaz = 0;
            //account.GetAccountAve(911010, 911130, ref untilDateBalance , ref untilDateBedehkar ,ref  emtiaz);
            Loan loan = repository.ActiveContext.BankAccounts.OfType<Loan>().FirstOrDefault(l => l.Code == "2/0/0");
            return emtiaz;
        }

        [Fact]
        public decimal getDayBalance()
        {
            AccountRepository repository = new AccountRepository();
            Account account =
                repository.ActiveContext.BankAccounts.OfType<Account>().FirstOrDefault(a => a.Code == "1/0/0");
            decimal aa = account.UntilDateBalance(911011);
            return aa;

        }

        [Fact]
        public string getsimpleBankAccount()
        {
            PersonAccountService ps = new PersonAccountService();
            SimpleAccountDto simpleAccountDto = ps.GetSimpleBankAccount("0/0/0");
            return simpleAccountDto != null ? simpleAccountDto.Description : "0";

        }

        [Fact]
        public string gerpersonsbyaccount()
        {
            PersonAccountService ps = new PersonAccountService();
            List<PersonDto> personDtos = ps.GetPersonByAccount("1/0/0");
            return "yes";
        }

    }
}
