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
            Correspondent correspondent = this.CreateCorrespondent();
            Account account = this.CreateAccount();
            account.Correspondents.Add(correspondent);

            context.Accounts.Add(account);

            context.SaveChanges();

            var anotherContext = new ValiasrContext("Valiasr.ce");
            Correspondent fetchedCorrespondent =
                anotherContext.Correspondents.FirstOrDefault(o => o.Id == correspondent.Id);
            Assert.True(correspondent.Equals(fetchedCorrespondent));
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
            context.Database.ExecuteSqlCommand("Delete from Persons");
            context.Database.ExecuteSqlCommand("Delete from Accounts");
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
                    ContactInfo = { Address = "babol", Tellno = 12435 }
                };
            return customer;
        }

        #endregion
    }
}