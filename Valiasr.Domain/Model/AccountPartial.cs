using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valiasr.Domain.Model
{
    public partial class Account:BankAccount
    {
        public decimal LastYearBalance(int yearOf)
        {
            return this.YearAccounts.Where(ya => ya.YearOf == yearOf).Any() ? this.YearAccounts.FirstOrDefault(ya => ya.YearOf == yearOf).Balance : 0;
        }

        public decimal LastYearBedehkar(int yearOf)
        {
            return this.YearAccounts.Where(ya => ya.YearOf == yearOf).Any() ? this.YearAccounts.FirstOrDefault(ya => ya.YearOf == yearOf).Bedehkar : 0;
        }

        public decimal LastYearBestankar(int yearOf)
        {
            return this.YearAccounts.Where(ya => ya.YearOf == yearOf).Any() ? this.YearAccounts.FirstOrDefault(ya => ya.YearOf == yearOf).Bestankar : 0;
        }

        public decimal TheDateBalance(int theDate)
        {
            int theYear = theDate / 10000;
            return this.LastYearBalance(theYear - 1) +
                   this.AccountActivities.Where(a => a.RegDate > (theYear * 10000 + 101) && a.RegDate <= theDate).Sum(a => a.Amount * a.Category);
        }

        public decimal GetAccountAve(int fromDate, int toDate)
        {
            return (decimal)fromDate + toDate;
        }
    }
}
