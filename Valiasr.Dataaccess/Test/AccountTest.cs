namespace Valiasr.DataAccess.Test
{
    using System;

    using Valiasr.Domain.Model;

    using Xunit;

    public class AccountTest : TestBase
    {
        [Fact]
        public void Create_Complex_Account()
        {
            var context = new ValiasrContext("Valiasr.ce");
            Person person = PersonTest.CreatePerson();
            Customer customer = PersonTest.CreateCustomer(person);
            Lawyer lawyer = PersonTest.CreateLawyer(person);
            GeneralAccount generalAccount = CreateGeneralAccount();
            IndexAccount indexAccount = CreateIndexAccount();
            Account account = CreateAccount();

            account.Lawyers.Add(lawyer);
            indexAccount.Accounts.Add(account);
            generalAccount.IndexAccounts.Add(indexAccount);

            context.GeneralAccounts.Add(generalAccount);
            account.Customers.Add(customer);
            context.SaveChanges();
        }

        public static Account CreateAccount()
        {
            var account = new Account { Id = Guid.NewGuid(), Balance = 1000, Description = "first", No = "1", IndexAccount = CreateIndexAccount()};
            return account;
        }

        public static GeneralAccount CreateGeneralAccount()
        {
            var generalAccount = new GeneralAccount { Description = "sandogh", Category = 1 };
            return generalAccount;
        }

        public static IndexAccount CreateIndexAccount()
        {
            var indexAccount = new IndexAccount { GeneralAccountCode = 0, GeneralAccount = CreateGeneralAccount()};
            return indexAccount;
        }
    }
}
