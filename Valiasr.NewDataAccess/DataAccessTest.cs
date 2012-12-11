namespace Valiasr.DataAccess
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Valiasr.NewDomain;

    [TestClass]
    public class DataAccessTest
    {
        #region Public Methods

        [TestMethod]
        public void Test_Database_Created()
        {
            var context = new ValiasrContext("Valiasr.Ce");
            var customer = this.CreateCustomer();
            context.Customers.Add(customer);
            context.SaveChanges();
            Assert.IsTrue(context.Database.Exists());
        }
        
        [TestMethod]
        public void Create_Relation_test()
        {
            var context = new ValiasrContext("Valiasr.ce");
            Customer customer = this.CreateCustomer();
            Account account = this.CreateAccount();
            account.Correspondents.Add(customer);

            context.Customers.Add(customer);
            context.Accounts.Add(account);

            context.SaveChanges();

            var anotherContext = new ValiasrContext("Valiasr.ce");
            var fetchedCustomer = anotherContext.Customers.FirstOrDefault(o => o.Id == customer.Id);
            Assert.IsTrue(customer.Equals(fetchedCustomer));
        }

        [TestCleanup]
        public void TestTearDown()
        {
            TearDown();
        }

        [ClassCleanup]
        public static void ClassTearDown()
        {
            TearDown();
        }
        #endregion

        #region Methods

        private static void TearDown()
        {
            var context = new ValiasrContext("Valiasr.Ce");
            context.Customers.SqlQuery("Delete from Persons");
            context.Accounts.SqlQuery("Delete from Accounts");
        }

        private Account CreateAccount()
        {
            var account = new Account { Id = Guid.NewGuid(), Balance = 1000, Description = "first",  No = "1"};
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

        #endregion
    }
}