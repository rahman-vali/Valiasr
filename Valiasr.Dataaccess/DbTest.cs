using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Valiasr.Domain;
using System.Linq;

namespace Valiasr.DataAccess
{
    [TestClass]
    public class DbTest
    {
        [TestMethod]
        public void Test_Database_Created()
        {
            var context = new ValiasrContext("Valiasr.Ce");
            var customer = new Customer
                {
                    Id = Guid.NewGuid(),
                    Firstname = "ali",
                    Lastname = "ahmadi",
                    ContactInfo = { Address = "babol", Tellno = 12435 }
                };
            context.Customers.Add(customer);
            context.SaveChanges();
            Assert.IsTrue(context.Database.Exists());
        }

        [TestMethod]
        public void Create_Relation_test()
        {
            var context = new ValiasrContext("Valiasr.ce");
            var customer = new Customer
                {
                    Id = Guid.NewGuid(),
                    Firstname = "ali"
                };
            var account = new Account
            {
                Id = Guid.NewGuid(),
                Hesab_Des = "first hesab",
                Sub_Des = "first"
            };

            var customerHesab = new CustomerHesab
           {
               Id = Guid.NewGuid(),
               Role = Role.SahebHesab,
               Customer = customer,
               Account = account
           };

            //context.Customers.Add(customer);
            context.CustomerHesabs.Add(customerHesab);

            context.SaveChanges();

            var anotherContext = new ValiasrContext("Valiasr.ce");
            Assert.IsTrue(customerHesab.Id == anotherContext.CustomerHesabs.FirstOrDefault(o => o.Id == customerHesab.Id).Id);

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

        private static void TearDown()
        {
            var context = new ValiasrContext("Valiasr.ce");
            context.CustomerHesabs.SqlQuery("Delete from CustomerHesabs");
            context.Customers.SqlQuery("Delete from Customers");
            context.Accounts.SqlQuery("Delete from Accounts");
        }
    }
}