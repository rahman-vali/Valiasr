
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
          public static IEnumerable<object[]> TesPersontData { get { yield return new object[] { Guid.Parse("eb706fde-6238-4add-8668-a52f460b94db") }; } }

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



         public static IEnumerable<object[]> TestAccountData { get { yield return new object[] { Guid.Parse("f422617f-76ab-4ed1-a366-56afc6e2eb98"), Guid.Parse("3cc23ade-b455-4496-a151-dce274c17622") }; } }

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
          public static IEnumerable<object[]> TestIndexAccountData { get { yield return new object[] { Guid.Parse("4dfe63d4-9816-4bd0-9051-9f4b4909a8cf") }; } }

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



        public static IEnumerable<object[]> TestPersonData { get { yield return new object[] { Guid.Parse("01edfdbd-a289-4ffe-9d07-c41a0de38052") }; } }

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
              personDto.NationalIdentity = "1";
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
            personDto.NationalIdentity = "21456457611";
            PersonAccountService pa = new PersonAccountService();
            string str = pa.AddPerson(personDto);
        }

        public static IEnumerable<object[]> TestRemovePerson { get { yield return new object[] { Guid.Parse("ca91fd3e-2729-4b77-b0cd-a4e415445853") }; } }

        [Theory]
        [PropertyData("TestRemovePerson")]
        public void removePerson(Guid id)
        {
            PersonAccountService personAccountService = new PersonAccountService();
            string str = personAccountService.RemovePerson(id);
        }

          public static IEnumerable<object[]> TestAddAccountData { get { yield return new object[] { Guid.Parse("f1ab901a-4674-4aaa-a057-bdcdc11f1c72") }; } }

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
              PersonDto personDto = pa.GetPersonByNationalIdentity("3");
              var a = personDto.NationalIdentity;

          }
        [Fact]
        public void GetIndexAccountTest()
        {
            PersonAccountService pa = new PersonAccountService();
            IndexAccountDto indexAccountDto = pa.GetIndexAccount("2/0");
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
            LoanRequestDto loanRequestDto = new LoanRequestDto {AccountCode = "1/0/0",Amount = 1000,Date = 13911102,Description = "vam" };
            PersonAccountService ps = new PersonAccountService();
            string str = ps.AddLoanRequest(loanRequestDto);
//            var context = new ValiasrContext("Valiasr");
//            var account = context.Accounts.Where(a => a.Code == "1/0/0").FirstOrDefault();
//            var loanRequest = this.Create_Request(account);
//            loanRequest.Account = account;
//    //        var loan = new Loan { Id = Guid.NewGuid(), Amount = 1000,LoanRequest = loanRequest,Account = account};
//      //      loanRequest.Loan = loan;
//            account.LoanRequests.Add(loanRequest);
//      //      context.LoanRequests.Add(loanRequest);
//            context.SaveChanges();

        }

        public static IEnumerable<object[]> RequestOky { get { yield return new object[] { Guid.Parse("1528fc7d-136b-4ac2-8ba1-0303a6c30560") }; } }

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


        [Fact]
        public void Creat_Loan()
        {
            var context = new ValiasrContext("Valiasr");
            var loanRequest = context.LoanRequests.Where(a => a.ReqNo == 1).FirstOrDefault();
            var account = context.BankAccounts.Where(a => a.Code == "1/0/0").FirstOrDefault();
            //var loan = new Loan { Id = Guid.NewGuid(), Amount = 2000 ,LoanRequest = loanRequest,Account = account};
            //loanRequest.Loan = loan;
        //    context.LoanRequests.Add(loanRequest);
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
   //         loanRequestOkyAsistant.OKyDate = 13921029;
            LoanRequest.LoanRequestOkyAsistant = loanRequestOkyAsistant;
            return LoanRequest;
        }

        [Fact]
        public decimal GetAve()
        {
            AccountRepository repository = new AccountRepository();
            Account account =
                repository.ActiveContext.BankAccounts.OfType<Account>().FirstOrDefault(a => a.Code == "1/0/0");
            decimal ave = account.GetAccountAve(10, 30);
            Loan loan = repository.ActiveContext.BankAccounts.OfType<Loan>().FirstOrDefault(l => l.Code == "2/0/0");

            return ave;
        }

        [Fact]
        public decimal getDayBalance()
        {
            AccountRepository repository = new AccountRepository();
            Account account =
                repository.ActiveContext.BankAccounts.OfType<Account>().FirstOrDefault(a => a.Code == "1/0/0");
            decimal aa = account.TheDateBalance(911011);
            return aa;

        }

    }
}
