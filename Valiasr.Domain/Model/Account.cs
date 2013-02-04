namespace Valiasr.Domain.Model
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class GeneralAccount:IAggregateRoot
    {
        public GeneralAccount()
        {
            Id = Guid.NewGuid();
            this.IndexAccounts = new Collection<IndexAccount>();
        }

        public static GeneralAccount CreateGeneralAccount(int code, string description, int category)
        {
            var generalAccount = new GeneralAccount { Code = code, Description = description, Category = category, };
            return generalAccount;
        }

        public Guid Id { get; set; }

        public int Code { get; set; }
        public string Description { get; set; }
        public int Category { get; set; }
        public int LastDate { get; set; }

        public Collection<IndexAccount> IndexAccounts { get; set; }

        public virtual bool ContainIndexAccount(string code)
        {
            //return (this.IndexAccounts.Count(ia => ia.Code == code) != 0);
            return IndexAccounts.Any(ia => ia.Code == code);
        }

        public bool ContainIndexAccounts
        {
            get
            {
                return this.IndexAccounts.Count() != 0;
            }
        }

        public bool CanBeSaved
        {
            get { return true; }
        }

        public bool CanBeDeleted
        {
            get { return !this.ContainIndexAccounts; }
        }
    }

    public class IndexAccount:IAggregateRoot
    {
        public IndexAccount()
        {
            Id = Guid.NewGuid();
            this.BankAccounts = new Collection<BankAccount>();
        }

        public static IndexAccount CreateIndexAccount(
            GeneralAccount generalAccount,
            string code,
            int generalAccountCode,
            int indexer,
            string description,
            bool haveAcounts)
        {
            var indexAccount = new IndexAccount
                {
                    GeneralAccount = generalAccount,
                    Code = code,
                    GeneralAccountCode = generalAccountCode,
                    RowId = indexer,
                    Description = description,
                    HaveAccounts = haveAcounts
                };
            return indexAccount;
        }
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int GeneralAccountCode { get; set; }
        public int RowId { get; set; }
        public string Description { get; set; }
        public short ExpiryDateCategory { get; set; }
        public int LastUpdated { get; set; }
        public bool HaveAccounts { get ; set; }

        public virtual GeneralAccount GeneralAccount { get; set; }
        public  Collection<BankAccount> BankAccounts { get; set; }

        public bool ContainAccounts
        {
            get
            {
                return this.BankAccounts.Count() != 0;
            }
        }
        public bool ContainAccount(string code)
        {            
            return this.BankAccounts.Where(ba => ba.Code == code).Count() != 0;
        }

        public bool CanBeSaved
        {
            get { return true ; }
        }

        public bool CanBeDeleted
        {
            get { return !ContainAccounts ; }
        }
    }

    public partial class BankAccount:IAggregateRoot
    {
        protected BankAccount()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string IndexAccountCode { get; set; }
        public string No { get; set; }
        public decimal Balance { get; set; }
        public string Description { get; set; }
        public string SubDescription { get; set; }
        public int LastDate { get; set; }
        public int BeginDate { get; set; }
        public int GroupId { get; set; }
        public decimal BodjetCode { get; set; }


        public virtual IndexAccount IndexAccount { get; set; }
        public virtual Collection<AccountActivity> AccountActivities { get; set; }
        public virtual Collection<YearAccount> YearAccounts { get; set; }
        public bool ContainActivities()
        {
            return this.AccountActivities.Any();      
        }
        public bool CanBeSaved
        {
            get { return true ; }
        }

        public bool CanBeDeleted
        {
            get { return !this.ContainActivities(); }
        }
    }

    public partial class Account:BankAccount
    {
        public Account()
        {
            this.Lawyers = new Collection<Lawyer>();
            this.Customers = new Collection<Customer>();
            this.LoanRequests = new Collection<LoanRequest>();
        }

        public static Account CreateAccount(IndexAccount indexAccount, string code, string no , int Indexer , decimal balance , string description)
        {
            var account = new Account
            {
                IndexAccount = indexAccount ,
                Code = code,
                No = no,
                Balance = balance,
                Description = description,
            };
            return account;
        }
        public decimal BottomAmount { get; set; }
        public decimal HebehQty { get; set; }
        public int StopDate { get; set; }
        public int PageNo { get; set; }
        public virtual Collection<Customer> Customers { get; set; }
        public virtual Collection<Lawyer> Lawyers { get; set; }
        public virtual Collection<LoanRequest> LoanRequests { get; set; }
        public virtual Collection<RequestAccountAve> RequestAccountAves { get; set; }
        
        public bool ContainCustomer(Guid personId)
        {
            return this.Customers.Any(c => c.Person.Id == personId);
        }

        public bool ContainLawyer(Guid personId)
        {
            return this.Lawyers.Any(l => l.Person.Id == personId);
        }

        public bool ContainLoanRequest(int loanRequestNo)
        {
            return this.LoanRequests.Any(lr => lr.ReqNo == loanRequestNo);
        }

        public bool Withdraw(string customerNo, decimal amount)
        {
            var customer = this.Customers
                .FirstOrDefault(o => o.No == customerNo);

            if (customer != null &&
                customer.Person.Balegh &&
                customer.HagheBardasht &&
                amount <= (decimal)customer.Portion / 100 * this.Balance)
            {
                return true;
            }
            return false;
        }


    }

    public class YearAccount
    {
        public Guid Id { get; set; }
        public string AccountCode { get; set; }
        public short YearOf { get; set; }
        public decimal Balance { get; set; }
        public decimal Bedehkar { get; set; }
        public decimal Bestankar { get; set; }
        public virtual BankAccount BankAccount { get; set; }
    }

}