namespace Valiasr.NewDataAccess
{
    using System;
    using System.Linq;

    using Valiasr.NewDomain;

    using Xunit;

    public class DataAccessTest
    {
        #region Constructors and Destructors

        public DataAccessTest()
        {
            TearDown();
        }

        #endregion

        #region Public Methods

        [Fact]
        public void Create_Relation_test()
        {
            var context = new ValiasrContext("Valiasr.ce");
            Customer customer = this.CreateCustomer();
            Vakil vakil = this.CreateVakil();
            Account account = this.CreateAccount();
            //account.Correspondents.Add(correspondent);
   //         Correspondent correspondent = customer;
            account.Correspondents.Add(vakil);
            context.Accounts.Add(account);
            //account.Correspondents.Add(customer);
            account.Correspondents.Add(customer);
        //    context.Accounts.Add(account);
  //          account.Correspondents.Add(vakil);
  //          context.Correspondents.Add(correspondent);
            context.Accounts.Add(account);
            Account account2 = this.CreateAccount();
            account2.Correspondents.Add(vakil);
            context.Accounts.Add(account2);
            context.SaveChanges();

            var anotherContext = new ValiasrContext("Valiasr.ce");
            //Correspondent fetchedCorrespondent =
              //  anotherContext.Correspondents.FirstOrDefault(o => o.Id == correspondent.Id);
            //Assert.True(correspondent.Equals(fetchedCorrespondent));
            Correspondent fetchedCorrespondent =
                anotherContext.Correspondents.FirstOrDefault(o => o.Id == customer.Id);
            Assert.True(customer.Equals(fetchedCorrespondent));
      //      account = this.CreateAccount();

        }

        [Fact]
        public void Test_Database_Created()
        {
            var context = new ValiasrContext("Valiasr.Ce");
            Customer customer = this.CreateCustomer();
            context.Correspondents.Add(customer);
            context.SaveChanges();
            Assert.True(context.Database.Exists());
        }

        #endregion

        #region Methods

        private static void TearDown()
        {
            var context = new ValiasrContext("Valiasr.Ce");
            context.Database.ExecuteSqlCommand("Delete from AccountCorrespondents");
            context.Database.ExecuteSqlCommand("Delete from Accounts");
            context.Database.ExecuteSqlCommand("Delete from Customers");
            context.Database.ExecuteSqlCommand("Delete From Vakils");
            context.Database.ExecuteSqlCommand("Delete from Persons");
        }

        private Account CreateAccount()
        {
            var account = new Account { Id = Guid.NewGuid(), Balance = 1000, Description = "first", No = "1" };
            return account;
        }

        private Correspondent CreateCorrespondent()
        {
            var correspondent = new Correspondent
                {
                    Id = Guid.NewGuid(),
                    Firstname = "ali",
                    Lastname = "ahmadi",
                    ContactInfo = { Address = "babol", Tellno = 12435 }
                };
            return correspondent;
        }

        private Customer CreateCustomer()
        {
            var customer = new Customer
                {
                    Id = Guid.NewGuid(),
                    Firstname = "ali",
                    Lastname = "ahmadi",
                    No = "1",
                    ContactInfo = { Address = "babol", Tellno = 12435 }
                };
            return customer;
        }

        private Vakil CreateVakil()
        {
            var vakil = new Vakil
            {
                Id = Guid.NewGuid(),
                Firstname = "ali2",
                Lastname = "ahmadi2",
                StartDate = new DateTime(2012,6,12),
                ContactInfo = { Address = "babol2", Tellno = 12435 }
            };
            return vakil;
        }

        #endregion
    }
}