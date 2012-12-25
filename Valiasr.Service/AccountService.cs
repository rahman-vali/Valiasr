namespace Valiasr.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Valiasr.DataAccess;
    using Valiasr.Domain;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.

    public class AccountService : IAccountService
    {
        #region Constants and Fields

        private readonly ValiasrContext Context = new ValiasrContext("Valiasr.Ce");

        #endregion

        #region Implemented Interfaces

        #region IAccountService

        public bool Bardasht(string account, double amount)
        {
            throw new NotImplementedException();
        }

        public bool CanBardasht(string accountNo, double amount)
        {
            Account acc = this.Context.Accounts.Include("Persons").FirstOrDefault(o => o.No == accountNo);
            if (acc == null)
            {
                throw new ArgumentNullException("Customer Does Not Exist");
            }
            return acc.Bardasht(accountNo, amount);
        }

        public ICollection<CustomerDto> GetCustomerByAccountNo(string accountNo)
        {
            return
                this.Context.Persons.Include("Accounts")
                    .Where(c => c.Accounts.Any(a => a.No == accountNo))
                    .OfType<Customer>()
                    .Select(o => new CustomerDto { HagheBardasht = o.HagheBardasht, No = o.No, Portion = o.Portion })
                    .ToList();
        }

        #endregion

        #endregion
    }
}