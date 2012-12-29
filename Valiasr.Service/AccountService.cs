namespace Valiasr.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Valiasr.DataAccess;
    using Valiasr.DataAccess.Repositories;
    using Valiasr.Domain.Model;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.

    public class AccountService : IAccountService
    {
        #region Constants and Fields

        private readonly ValiasrContext context = new ValiasrContext("Valiasr.Ce");

        #endregion

        #region Implemented Interfaces

        #region IAccountService

        public bool Bardasht(string account, double amount)
        {
            throw new NotImplementedException();
        }

        public bool CanBardasht(string accountNo, double amount)
        {
            Account acc = this.context.Accounts.Include("RelativePersons").FirstOrDefault(o => o.No == accountNo);
            if (acc == null)
            {
                throw new ArgumentNullException("Customer Does Not Exist");
            }
            return acc.Bardasht(accountNo, amount);
        }

        public ICollection<CustomerDto> GetCustomerByAccountNo(string accountNo)
        {
            return
                this.context.Accounts.Include("Customers")
                    .Where(c => c.No == accountNo)
                    .SelectMany(o => o.Customers)
                    .Select(o => new CustomerDto { HagheBardasht = o.HagheBardasht, No = o.No, Portion = o.Portion })
                    .ToList();
        }

        #endregion

        public void AddGeneralAccount(GeneralAccountDto generalAccountDto)
        {
            GeneralAccount generalAccount = new GeneralAccount();
            TranslateGeneralAccountDtoToGeneralAccount(generalAccountDto, generalAccount);
            GeneralAccountRepository repository = new GeneralAccountRepository();
            repository.Add(generalAccount);
        }


        public void AddIndexAccount(IndexAccountDto indexAccountDto)
        {
            IndexAccount indexAccount = new IndexAccount();
            this.TranslateIndexAccountDtoToIndexAccount(indexAccountDto, indexAccount);
            GeneralAccountRepository repository = new GeneralAccountRepository();
            repository.AddIndexAccount(indexAccount);
        }


        public void AddAccount<T>(T obj)
        {

        }

        private void TranslateGeneralAccountDtoToGeneralAccount(
            GeneralAccountDto generalAccountDto, GeneralAccount generalAccount)
        {

        }

        private void TranslateIndexAccountDtoToIndexAccount(IndexAccountDto indexAccountDto, IndexAccount indexAccount)
        {

        }

        #endregion
    }

}