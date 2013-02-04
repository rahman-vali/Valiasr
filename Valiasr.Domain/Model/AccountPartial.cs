using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valiasr.Domain.Model
{
    public partial class BankAccount
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

        public decimal UntilDateBalance(int theDate)
        {
            int theYear = theDate / 10000;
            return this.LastYearBalance(theYear - 1) +
                   this.AccountActivities.Where(a => a.RegDate > (theYear * 10000 + 101) && a.RegDate <= theDate).Sum(a => a.Amount * a.Category);
        }
    }

    public partial class Account:BankAccount
    {
        public void GetAccountAve(int fromDate, int toDate , ref decimal untilDateBalance , ref decimal untilDateBedehkar , ref decimal emtiaz)
        {
            int begDate = fromDate;
            untilDateBalance = this.UntilDateBalance(fromDate);
            untilDateBedehkar = this.LastYearBedehkar((fromDate / 10000) - 1) + (Math.Abs(this.LastYearBalance((fromDate / 10000) - 1)) < Math.Abs(untilDateBalance) ?  Math.Abs(untilDateBalance) - Math.Abs(this.LastYearBalance((fromDate / 10000) - 1)) : 0);
            var dayActivities = this.getDayActivities(fromDate, toDate);
            foreach (var dayActivitiy in dayActivities)
            {
                emtiaz = emtiaz + untilDateBalance * (dayActivitiy.date - begDate);
                untilDateBalance = untilDateBalance + dayActivitiy.dayBalance;
                if (dayActivitiy.dayBalance > 0) untilDateBedehkar = untilDateBedehkar + dayActivitiy.dayBalance;
                begDate = dayActivitiy.date;
            }
            emtiaz = emtiaz + untilDateBalance * (toDate - begDate);
        }

        private List<dynamic> getDayActivities(int fromDate, int toDate)
        {
            return new List<dynamic>(this.AccountActivities.Where(a => a.RegDate >= fromDate && a.RegDate <= toDate)
                                            .GroupBy(a => a.RegDate)
                                            .Select(n => new { date = n.Key , dayBalance = n.Sum(b => b.Amount * b.Category) }).OrderBy( a => a.date)
                                            .ToList());
        }
         
    }
}
