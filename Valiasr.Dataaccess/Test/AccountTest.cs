namespace Valiasr.DataAccess.Test
{
    using System;

    using Valiasr.DataAccess.Repositories;
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
            IndexAccount indexAccount = CreateIndexAccount();
            GeneralAccount generalAccount = CreateGeneralAccount();
            Account account = CreateAccount();
            account.Lawyers.Add(lawyer);
            account.Customers.Add(customer);
            indexAccount.Accounts.Add(account);
            generalAccount.IndexAccounts.Add(indexAccount);
            context.GeneralAccounts.Add(generalAccount);
            //context.IndexAccounts.Add(indexAccount);
            context.SaveChanges();
           /* GeneralAccount generalAccount = CreateGeneralAccount();



            account.Lawyers.Add(lawyer);
            indexAccount.Accounts.Add(account);
            generalAccount.IndexAccounts.Add(indexAccount);

            context.GeneralAccounts.Add(generalAccount);
            account.Customers.Add(customer);
            context.SaveChanges();*/
        }

        [Fact]
        public void Create_Complex_Account1()
        {
            GeneralAccountRepository context = new GeneralAccountRepository();
            Person person = PersonTest.CreatePerson();
 //           Customer customer = PersonTest.CreateCustomer(person);
            Lawyer lawyer = PersonTest.CreateLawyer(person);
            GeneralAccount generalAccount = CreateGeneralAccount();
            IndexAccount indexAccount = CreateIndexAccount();
            Account account = CreateAccount();
//            account.Customers.Add(customer);
            account.Lawyers.Add(lawyer);
            indexAccount.Accounts.Add(account);
            generalAccount.IndexAccounts.Add(indexAccount);
            context.Add(generalAccount);

            //context.AddIndexAccount(indexAccount);
            //context.GeneralAccounts.Add(generalAccount);

           // context.SaveChanges();
        }

        [Fact]
        public void Create_Complex_Account2()
        {
            PersonRepository context = new PersonRepository();
            Person person = PersonTest.CreatePerson();
            Customer customer = PersonTest.CreateCustomer(person);
            Lawyer lawyer = PersonTest.CreateLawyer(person);
            if (person != null)
            {
                context.Add(person);
            }
            /*            GeneralAccount generalAccount = CreateGeneralAccount();
            IndexAccount indexAccount = CreateIndexAccount();
            Account account = CreateAccount();
            indexAccount.Accounts.Add(account);
            generalAccount.IndexAccounts.Add(indexAccount);
            context.Add(generalAccount);
            CustomerRepository customerRepository = new CustomerRepository();
            customer.Accounts.Add(account);
            customerRepository.Add(customer);
            LawyerRepository lawyerRepository = new LawyerRepository();
            lawyer.Accounts.Add(account);
            lawyerRepository.Add(lawyer);*/
        }
        [Fact]
        public void create_generalAccount()
        {
            GeneralAccount generalAccount = CreateGeneralAccount();
            GeneralAccountRepository repository = new GeneralAccountRepository();
            repository.Add(generalAccount);
        }

        [Fact]
        public void create_IndexAccount()
        {
            this.create_generalAccount();
            IndexAccount indexAccount = CreateIndexAccount();
            GeneralAccountRepository repository = new GeneralAccountRepository();
            //repository.AddIndexAccount(indexAccount);

        }

        [Fact]
        public void Create_Account()
        {
            this.create_IndexAccount();
            Account account = CreateAccount();
            IndexAccountRepository repository = new IndexAccountRepository();
            //repository.AddAccount(account);
        }
        public static Account CreateAccount()
        {
            var account = new Account { Id = Guid.NewGuid(), Balance = 1000, Description = "first", No = "1",IndexAccountCode = "1/0"};
            return account;
        }

        public static GeneralAccount CreateGeneralAccount()
        {
            var generalAccount = new GeneralAccount { Description = "sandogh", Category = 1,Code = 1};
            return generalAccount;
        }

        public static IndexAccount CreateIndexAccount()
        {
            var indexAccount = new IndexAccount { GeneralAccountCode = 1,Code = "1/0"};
            return indexAccount;
        }
    }
}
