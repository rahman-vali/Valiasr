namespace Valiasr.DataAccess.Test
{
    using System;
    using System.Collections.ObjectModel;

    using Valiasr.DataAccess.Repositories;
    using Valiasr.Domain.Model;
    using System.Linq;

    using Xunit;

    public class AccountTest : TestBase
    {
        [Fact]
        public void Create_Complex_Account()
        {
            var context = new ValiasrContext("Valiasr");
            Person person = PersonTest.CreatePerson();
            Customer customer = PersonTest.CreateCustomer(person);
            Lawyer lawyer = PersonTest.CreateLawyer(person);
            IndexAccount indexAccount = CreateIndexAccount();
            GeneralAccount generalAccount = CreateGeneralAccount();
            Account account = CreateAccount();
            account.Lawyers = new Collection<Lawyer>();
            account.Lawyers.Add(lawyer);
            account.Customers = new Collection<Customer>();
            account.Customers.Add(customer);
            LoanRequest loanRequest = new LoanRequest();
            Loan loan = new Loan();
    //        loan.Account = account;
            loan.LoanRequest = loanRequest;
            LoanRequestOkyAssistant loanRequestOkyAssistant = new LoanRequestOkyAssistant();
            loanRequest.LoanRequestOkyAsistant = loanRequestOkyAssistant;
            loanRequest.Account = account;
            account.LoanRequests = new Collection<LoanRequest>();
            account.LoanRequests.Add(loanRequest);
            indexAccount.BankAccounts = new Collection<BankAccount>();
            indexAccount.BankAccounts.Add(account);
            indexAccount.BankAccounts.Add(loan);
            generalAccount.IndexAccounts = new Collection<IndexAccount>();
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
            indexAccount.BankAccounts.Add(account);
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

        [Fact]

        public static Account CreateAccount()
        {
            var account = new Account { Id = Guid.NewGuid(),Balance = 1000, Description = "first", No = "1",IndexAccountCode = "1/0",Code = "1/0/0"};
            return account;
        }

        public static GeneralAccount CreateGeneralAccount()
        {
            var generalAccount = new GeneralAccount { Id = Guid.NewGuid(),Description = "sandogh", Category = 1,Code = 1};
            return generalAccount;
        }

        public static IndexAccount CreateIndexAccount()
        {
            var indexAccount = new IndexAccount { Id = Guid.NewGuid() ,GeneralAccountCode = 1,Code = "1/0"};
            return indexAccount;
        }
    }
}
