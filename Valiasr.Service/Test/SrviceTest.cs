
namespace Valiasr.Service.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
                  var accounts = repository.ActiveContext.Accounts.Include("Customers")
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


          public static IEnumerable<object[]> TestAccountData { get { yield return new object[] { Guid.Parse("cb6ea868-cee7-443f-a4bc-3b107d98a6b8"), Guid.Parse("b2d93f0b-8af1-4228-bbb7-2c9ab96b64ad") }; } }

          [Theory]
          [PropertyData("TestAccountData")]
          public string AddCustomerToAccount(Guid accountId, Guid id)
          {
              
                  AccountRepository repository = new AccountRepository();
                  Person person = repository.ActiveContext.Persons.Where(p => p.Id == id).FirstOrDefault();
                  if (person == null) return ("the person id is invalid and person can't be find");
                  Account account = repository.ActiveContext.Accounts.Include("Customers").Where(a => a.Id == accountId).FirstOrDefault();
                  if (account == null) return "account id is invalid and can not be find";
                  string messageStr = "";
                  if (account.AddCustomer(person, "200", 1, ref messageStr)) repository.ActiveContext.SaveChanges();
                  return messageStr;
              
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
              GeneralAccountRepository repository = new GeneralAccountRepository();
                   Assert.True(repository.ActiveContext.GeneralAccounts.Where(ga => ga.Code == 15).Count() == 1);

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
              personDto.NationalIdentity = "2145645789";
              PersonAccountService pa = new PersonAccountService();
              string str = pa.AddPerson(personDto);
          }

        [Theory]
        [PropertyData("TestPersonData")]
        public void AddPersonTest(Guid id)
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
            personDto.NationalIdentity = "2145645789";
            PersonAccountService pa = new PersonAccountService();
            string str = pa.AddPerson(personDto);
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
    }
}
