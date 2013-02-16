namespace Valiasr.Domain.Model
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class GeneralAccount:IAggregateRoot
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public int Category { get; set; }
        public int LastDate { get; set; }

        public Collection<IndexAccount> IndexAccounts { get; set; }

        public virtual bool ContainIndexAccount(string code)
        {
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
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int GeneralAccountCode { get; set; }
        public int RowId { get; set; }
        public string Description { get; set; }
        public short ExpiryDateCategory { get; set; }
        public int LastUpdated { get; set; }
        public bool HaveAccounts { get ; set; }

        public virtual GeneralAccount GeneralAccount { get; set; }
        public virtual Collection<BankAccount> BankAccounts { get; set; }

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
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string IndexAccountCode { get; set; }
        public int RowId { get; set; }
        public string No { get; set; }
        public decimal Balance { get; set; }
        public string Description { get; set; }
        public string SubDescription { get; set; }
        public int LastDate { get; set; }
        public int BeginDate { get; set; }
        public int GroupId { get; set; }
        public decimal BodjetCode { get; set; }


        public virtual IndexAccount IndexAccount { get; set; }
        public  virtual Collection<AccountActivity> AccountActivities { get; set; }
        public  virtual Collection<YearAccount> YearAccounts { get; set; }
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