using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valiasr.DataAccess.Repositories
{
    using Valiasr.Domain.Model;
    using Valiasr.Domain.Repositories;

    public class GeneralAccountRepository:Repository<GeneralAccount>,IGeneralAccountRepository
    {
        public void AddIndexAccount(IndexAccount indexAccount)
        {
            GeneralAccount generalAccount =
                ActiveContext.GeneralAccounts.Where(ga => ga.Code == indexAccount.GeneralAccountCode).FirstOrDefault();
            if (generalAccount == null)
                throw new Exception("the index account generaaccountcode is invalid and is not any generalaccount in ");
            generalAccount.IndexAccounts.Add(indexAccount);
            ActiveContext.SaveChanges();
        }
    }
}
