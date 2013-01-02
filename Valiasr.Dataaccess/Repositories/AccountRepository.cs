using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valiasr.DataAccess.Repositories
{
    using Valiasr.Domain.Model;
    using Valiasr.Domain.Repositories;

    public class AccountRepository:Repository<Account>,IAccountRepository
    {

    }
}
