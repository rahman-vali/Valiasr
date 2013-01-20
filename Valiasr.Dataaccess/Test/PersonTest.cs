namespace Valiasr.DataAccess.Test
{
    using System;
    using System.Linq;

    using Valiasr.DataAccess.Repositories;
    using Valiasr.Domain.Model;

    using Xunit;

    public class PersonTest : TestBase
    {

        ValiasrContext context = new ValiasrContext("Valiasr.ce");
        ValiasrContext anotherContext = new ValiasrContext("Valiasr.ce");

        /// <summary>
        /// Sakhtan yek vakil va ezafe kardane an dar 2 hesab
        /// </summary>
        [Fact]
        public void Create_One_Lawyer_In_Two_Accounts()
        {
            Customer customer = CreateCustomer(CreatePerson());
            Lawyer lawyer = CreateLawyer(CreatePerson());

            Account account1 = AccountTest.CreateAccount();
            account1.Lawyers.Add(lawyer);
            account1.Customers.Add(customer);
            this.context.Accounts.Add(account1);

            Account account2 = AccountTest.CreateAccount();
            account2.Lawyers.Add(lawyer);
            this.context.Accounts.Add(account2);

            this.context.SaveChanges();

            Assert.True(this.anotherContext.Persons.Count() == 2);
            Assert.True(this.anotherContext.Accounts.Count() == 2);
            var lawyers = this.anotherContext.Accounts.SelectMany(o => o.Lawyers).Distinct().ToList();
            Assert.True(lawyers.Count() == 1);
            Assert.True(this.anotherContext.Accounts.SelectMany(o=> o.Customers).Count() == 1);
        }

        [Fact]
        public void Add_Single_Person_As_Customer_And_Lawyer()
        {
            var person = CreatePerson();
            var lawyer = CreateLawyer(person);
            var customer = CreateCustomer(person);
            
            var account = AccountTest.CreateAccount();
            account.Lawyers.Add(lawyer);
            account.Customers.Add(customer);
            
            this.context.Accounts.Add(account);
            this.context.SaveChanges();

            Assert.True(this.anotherContext.Accounts.SelectMany(o => o.Lawyers).Distinct().Count() == 1);
            Assert.True(this.anotherContext.Accounts.SelectMany(o => o.Customers).Count() == 1);
            Assert.True(this.anotherContext.Accounts.Count() == 1);
        }

        [Fact]
        public void Add_Single_Person_As_Customer_And_Lawyer_2()
        {
            var person = CreatePerson();
            var lawyer = Lawyer.CreateLawyer(person,new DateTime(2012,12,20));
            var customer = Customer.CreateCustomer(person,"3" , 1);

            var account = AccountTest.CreateAccount();
            account.Lawyers.Add(lawyer);
            account.Customers.Add(customer);
       //     Assert.True(this.anotherContext.Accounts.SelectMany(o => o.Lawyers).Distinct().Count() == 1);
         //   Assert.True(this.anotherContext.Accounts.SelectMany(o => o.Customers).Count() == 1);
           // Assert.True(this.anotherContext.Accounts.Count() == 1);
        }
 

        public static Person CreatePerson()
        {
            return new Person()
                {
                    IndivOrOrgan = 1,
                    Firstname = "ali",
                    Lastname = "ahmadi",
                    ContactInfo = new ContactInfo() { HomeAddress = "babol", HomeTelno = "12435" }
                };
        }

        public string GenerateRandomString()
        {
            return new string(this.GenerateRandomString(15).ToArray());
        }

        public static Customer CreateCustomer(Person person)
        {
            var customer = new Customer
                {
                    Id = Guid.NewGuid(),
                    Person = person,
                    No = "1",
                };
            return customer;
        }

        public static Lawyer CreateLawyer(Person person)
        {
            var vakil = new Lawyer
            {
                Id = Guid.NewGuid(),
                StartDate = new DateTime(2012, 6, 12),
                Person = person,
            };
            return vakil;
        }        
    }
}