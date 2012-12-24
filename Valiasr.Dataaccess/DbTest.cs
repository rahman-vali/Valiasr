using System;
using System.Linq;
    //using Microsoft.VisualStudio.TestTools.UnitTesting;
using Valiasr.Domain;
using NUnit.Framework;
namespace Valiasr.DataAccess
{

    public class DbTest
    {
        #region Constructors and Destructors

        public DbTest()
        {
            TearDown();
        }

        #endregion

        #region Public Methods

        [Test]
        public void CreateRelationtest()
        {
            var context = new ValiasrContext("Valiasr.ce");
            Customer customer = Customer.CreateCustomer("ali", "ahmadi", "babol", "2" , "2");  //this.CreateCustomer();
            Correspondent correspondent = customer;// vakil;//new Correspondent();
            context.Correspondents.Add(correspondent);
            context.SaveChanges();
            /*Vakil vakil = Vakil.CreateVakil("ahmad", "haghighi", "tehran", new DateTime(2012, 09, 28) , "1");//this.CreateVakil();
            Kol kol = this.CreateKol();
            Moin moin = this.CreateMoin();           
            Account account = this.CreateAccount();
            Correspondent correspondent = vakil;//new Correspondent();

            account.Correspondents.Add(vakil);
            account.Correspondents.Add(customer);
            moin.Accounts.Add(account);
            kol.Moins.Add(moin); 
            context.Kols.Add(kol);
       //     Account account2 = this.CreateAccount();
         //   account2.Correspondents.Add(vakil);
           // moin.Accounts.Add(account2);
            //kol.Moins.Add(moin);
            //account2.Moin = moin;
            //context.Accounts.Add(account2);
            context.Kols.Add(kol);
            //context.Accounts.Add(account);
            context.SaveChanges();

            var anotherContext = new ValiasrContext("Valiasr.ce");
            Correspondent fetchedCorrespondent = anotherContext.Correspondents.FirstOrDefault(o => o.Id == correspondent.Id);
            Assert.True(correspondent.Equals(fetchedCorrespondent));
          /*  context.Accounts.Remove(account);
            context.Moins.Remove(moin);
            context.Kols.Remove(kol);
            context.Correspondents.Remove(vakil);
            context.Correspondents.Remove(customer);
            context.SaveChanges();*/
        }

        [Test]
        public void TestDatabaseCreated()
        {
            var context = new ValiasrContext("Valiasr.Ce");
            Customer customer = this.CreateCustomer();
            context.Correspondents.Add(customer);
            context.SaveChanges();
            Assert.True(context.Database.Exists());
        }

        #endregion

        #region Methods
        [SetUp]
        private static void TearDown()
        {
            var context = new ValiasrContext("Valiasr.Ce");
            context.Database.ExecuteSqlCommand("Delete from AccountCorrespondents");
            context.Database.ExecuteSqlCommand("Delete from Accounts");
            context.Database.ExecuteSqlCommand("Delete from Customers");
            context.Database.ExecuteSqlCommand("Delete From Vakils");
            context.Database.ExecuteSqlCommand("Delete from Persons");
            context.Database.ExecuteSqlCommand("Delete from Moins");
            context.Database.ExecuteSqlCommand("Delete from Kols");            
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

        private Correspondent CreateCorrespondent()
        {
            var correspondent = new Correspondent
                {
                    
                    Id = Guid.NewGuid(),
                    Firstname = "ali",
                    Lastname = "ahmadi",
                    MelliIdentity = "4",
                    ContactInfo = { HomeAddress = "babol", HomeTelno = "12435" }
                };
            return correspondent;
        }

        private Customer CreateCustomer()
        {
            var customer = Customer.CreateCustomer("ali", "ahmadi", "babol", "1", "3");
           /* var customer = new Customer
                {
                    
                    Id = Guid.NewGuid(),
                    Firstname = "ali",
                    Lastname = "ahmadi",
                    No = "1",
                    ContactInfo = { Address = "babol", Tellno = 12435 }
                };*/
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
                MelliIdentity = "5",
                ContactInfo = { HomeAddress = "babol2", HomeTelno = "12435" }
            };
            return vakil;
        }

        #endregion
    }
}
