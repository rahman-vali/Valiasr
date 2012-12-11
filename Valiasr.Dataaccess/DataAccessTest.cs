namespace Valiasr.DataAccess
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Valiasr.Domain;
    using Valiasr.Domain.SystemJari;

    [TestClass]
    public class DataAccessTest
    {
        #region Public Methods

        [ClassCleanup]
        public static void ClassTearDown()
        {
            TearDown();
        }

        [TestMethod]
        public void Create_Relation_test()
        {
            var context = new ValiasrContext("Valiasr.ce");
            Customer customer = this.CreateCustomer();
            Account account = this.CreateAccount();
            CustomerHesab customerHesab = this.CreateCustomerHesab(customer, account);

            //context.Customers.Add(customer);
            context.CustomerHesabs.Add(customerHesab);

            context.SaveChanges();

            var anotherContext = new ValiasrContext("Valiasr.ce");
            Assert.IsTrue(customerHesab == anotherContext.CustomerHesabs.FirstOrDefault(o => o.Id == customerHesab.Id));
        }

        [TestCleanup]
        public void TestTearDown()
        {
            TearDown();
        }

        [TestMethod]
        public void Test_Database_Created()
        {
            var context = new ValiasrContext("Valiasr.Ce");
            var customer = this.CreateCustomer();
            context.Customers.Add(customer);
            context.SaveChanges();
            Assert.IsTrue(context.Database.Exists());
        }

        #endregion

        #region Methods

        private static void TearDown()
        {
            var context = new ValiasrContext("Valiasr.ce");
            context.CustomerHesabs.SqlQuery("Delete from CustomerHesabs");
            context.Customers.SqlQuery("Delete from Customers");
            context.Accounts.SqlQuery("Delete from Accounts");
        }

        private Account CreateAccount()
        {
            var account = new Account { Id = Guid.NewGuid(), Hesab_Des = "first hesab", Sub_Des = "first" };
            return account;
        }

        private Customer CreateCustomer()
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Firstname = "ali",
                Lastname = "ahmadi",
                ContactInfo = { Address = "babol", Tellno = 12435 }
            };
            return customer;
        }

        private CustomerHesab CreateCustomerHesab(Customer customer, Account account)
        {
            var customerHesab = new CustomerHesab
                {
                    Id = Guid.NewGuid(),
                    Role = Role.SahebHesab,
                    Customer = customer,
                    Account = account
                };
            return customerHesab;
        }

        #endregion
    }
}