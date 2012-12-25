namespace Valiasr.DataAccess
{
    using System;
    using System.Linq;

    using NUnit.Framework;

    using Valiasr.Domain;

    public class DataAccessTest
    {
        #region Constructors and Destructors


        public DataAccessTest()
        {
            TearDown();
        }

        #endregion

        #region Public Methods

        [Test]
        public void Create_Relation_test()
        {
            var context = new ValiasrContext("Valiasr.ce");
            Customer customer = this.CreateCustomer();
            Vakil vakil = this.CreateVakil();
            Kol kol = this.CreateKol();
            Moin moin = this.CreateMoin();
            Account account = this.CreateAccount();
            //account.Persons.Add(correspondent);
   //         Correspondent correspondent = customer;
            account.Persons.Add(vakil);
            context.Accounts.Add(account);
            //account.Persons.Add(customer);
            account.Persons.Add(customer);
        //    context.Accounts.Add(account);
  //          account.Persons.Add(vakil);
  //          context.Persons.Add(correspondent);
            context.Accounts.Add(account);
            Account account2 = this.CreateAccount();
            account2.Persons.Add(vakil);
            context.Accounts.Add(account2);
            context.SaveChanges();

            var anotherContext = new ValiasrContext("Valiasr.ce");
            //Correspondent fetchedCorrespondent =
              //  anotherContext.Persons.FirstOrDefault(o => o.Id == correspondent.Id);
            //Assert.True(correspondent.Equals(fetchedCorrespondent));
            Person fetchedCorrespondent =
                anotherContext.Persons.FirstOrDefault(o => o.Id == customer.Id);
            Assert.True(customer.Equals(fetchedCorrespondent));
      //      account = this.CreateAccount();

        }

        [Test]
        public void Test_Database_Created()
        {
            var context = new ValiasrContext("Valiasr.Ce");
            Customer customer = this.CreateCustomer();
            context.Persons.Add(customer);
            context.SaveChanges();
            Assert.True(context.Database.Exists());
        }

        #endregion

        #region Methods

        [SetUp]
        private static void TearDown()
        {
            var context = new ValiasrContext("Valiasr.Ce");
            context.Database.ExecuteSqlCommand("Delete from AccountPersons");
            context.Database.ExecuteSqlCommand("Delete from Accounts");
            context.Database.ExecuteSqlCommand("Delete from Customers");
            context.Database.ExecuteSqlCommand("Delete From Vakils");
            context.Database.ExecuteSqlCommand("Delete from Persons");
            context.Database.ExecuteSqlCommand("Delete from Kols");
            context.Database.ExecuteSqlCommand("Delete from Moins");
        }

        private Account CreateAccount()
        {
            var account = new Account { Id = Guid.NewGuid(), Balance = 1000, Description = "first", No = "1" };
            return account;
        }

        private Kol CreateKol()
        {
            var kol = new Kol { Id = 1, Description = "sandogh", Kind = 1 };
            return kol;
        }

        private Moin CreateMoin()
        {
            var moin = new Moin { Id = 1, Hesab_Have = false, Moin_InKol_Code = 0 };
            return moin;
        }

        private Person CreateCorrespondent()
        {
            var correspondent = new Person()
                {
                    Id = Guid.NewGuid(),
                    Firstname = "ali",
                    Lastname = "ahmadi",
                    ContactInfo = { HomeAddress = "babol", HomeTelno = "12435" }
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
                    ContactInfo = { HomeAddress = "babol", HomeTelno = "12435" }
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
                ContactInfo = { HomeAddress = "babol2", HomeTelno = "12435" }
            };
            return vakil;
        }

        #endregion
    }
}