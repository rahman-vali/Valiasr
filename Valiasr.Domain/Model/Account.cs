﻿namespace Valiasr.Domain.Model
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
        public Guid Id { get; set; }

        public int Code { get; set; }
        public string Description { get; set; }
        public int Category { get; set; }
        public int LastDate { get; set; }

        public Collection<IndexAccount> IndexAccounts { get; set; }

        public bool CanBeSaved
        {
            get { return true; }
        }

        public bool CanBeDeleted
        {
            get { return true; }
        }
    }

    public class IndexAccount:IAggregateRoot
    {
        public IndexAccount()
        {
            Id = Guid.NewGuid();
            this.Accounts = new Collection<Account>();
        }
        public Guid Id { get; set; }

        public string Code { get; set; }
        public int GeneralAccountCode { get; set; }
        public int Indexer { get; set; }
        public string Description { get; set; }
        public short ExpiryDateCategory { get; set; }
        public int LastUpdated { get; set; }
        public bool HaveAccounts { get ; set; }

        public GeneralAccount GeneralAccount { get; set; }
        public Collection<Account> Accounts { get; set; }

        public bool CanBeSaved
        {
            get { return true ; }
        }

        public bool CanBeDeleted
        {
            get { return true ; }
        }
    }

    public class Account:IAggregateRoot
    {
        public Account()
        {
            Id = Guid.NewGuid();
            this.Lawyers = new Collection<Lawyer>();
            this.Customers = new Collection<Customer>();
        }

        public Guid Id { get; set; }
        //Shomare Hesab
        public string IndexCode { get; set; }
        public string Code { get; set; }
        public string No { get; set; }
        /// Mojodi Hesab
        public double Balance { get; set; }
        public string Description { get; set; }

        public virtual IndexAccount IndexAccount { get; set; }
        public Collection<Customer> Customers { get; set; }
        public Collection<Lawyer> Lawyers { get; set; }

        public bool Bardasht(string customerNo, double amount)
        {
            var customer = this.Customers
                .FirstOrDefault(o => o.No == customerNo);

            if (customer != null &&
                customer.Person.Balegh &&
                customer.HagheBardasht &&
                amount <= customer.Portion / 100 * this.Balance)
            {
                return true;
            }
            return false;
        }

        public bool CanBeSaved
        {
            get { return true ; }
        }

        public bool CanBeDeleted
        {
            get { return true ; }
        }
    }


}