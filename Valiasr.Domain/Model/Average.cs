namespace Valiasr.Domain.Model
{
    using System;
    using System.Collections.Generic;

    public class Average
    {
        public Average(string name)
        {
            Name = name;
        }

        public Average(BankAccount bank , int fromDate , int toDate)
        {
            begDate = fromDate;
            endDate = toDate;
            untilDateBalance = bankAccount.UntilDateBalance(begDate);
            untilDateBedehkar = bankAccount.UntilDateBedehkar(begDate);
            daysBalance = bankAccount.getActivities(begDate, endDate);
        }
        public int Id { get; set; }
        public string Name { get; set; }

        protected  BankAccount bankAccount;

        protected int begDate;

        protected int endDate;

        protected decimal untilDateBalance;

        protected decimal untilDateBedehkar;

        protected decimal emtiaz;

        protected List<dynamic> daysBalance;

        public virtual void CalcEmtiz()
        {
        }
    }

    public class NormAverage : Average
    {
        public NormAverage(string name):base(name){}
        public NormAverage(BankAccount bank , int fromDate , int toDate):base(bank ,fromDate , toDate){}

        public override void CalcEmtiz()
        {
//            var dayActivities = bankAccount.getActivities(begDate, endDate);
            foreach (var dayBalance in daysBalance)
            {
                emtiaz = emtiaz + untilDateBalance * (dayBalance.date - begDate);
                untilDateBalance = untilDateBalance + dayBalance.DayBalance;
                if (dayBalance.DayBalance > 0) untilDateBedehkar = untilDateBedehkar + dayBalance.DayBalance;
                begDate = dayBalance.Date;
            }
            emtiaz = emtiaz + untilDateBalance * (endDate - begDate);
        }

    }

    public class AverageWithMin : Average
    {
        public AverageWithMin(string name):base(name){}
        public AverageWithMin(BankAccount bank , int fromDate , int toDate):base(bank ,fromDate , toDate){}
        public override void CalcEmtiz()
        {
//            var dayActivities = bankAccount.getActivities(begDate, endDate );
            decimal dayMinActivity = 0;
            foreach (var dayBalance in daysBalance)
            {
                emtiaz = emtiaz + (untilDateBalance + dayMinActivity);
                emtiaz = emtiaz + untilDateBalance * ((dayBalance.date - begDate) - 1);
                untilDateBalance = untilDateBalance + dayBalance.DayBalance;
                if (dayBalance.DayBalance > 0) untilDateBedehkar = untilDateBedehkar + dayBalance.DayBalance;
                dayMinActivity = GetMinDayBalance(bankAccount, begDate, untilDateBalance);
                begDate = dayBalance.Date;
            }
            emtiaz = emtiaz + untilDateBalance * (endDate - begDate);
        }
        private decimal GetMinDayBalance(BankAccount bankAccount, int theDate , decimal untilDateBalance)
        {
            var dayActivities = bankAccount.getDayActivities(theDate);
            if (dayActivities.Count == 1) return 0;
            decimal stepBalance =untilDateBalance;
            decimal minBalance = untilDateBalance;
            foreach (var dayActivity in dayActivities)
            {
                stepBalance = stepBalance + dayActivity.timeBalance;
                if (Math.Abs(minBalance) < Math.Abs(stepBalance)) minBalance = stepBalance;
            }
            return minBalance;
        }

    }

    public class AverageWithHoliday : Average
    {
        public AverageWithHoliday(string name):base(name){}
        public override void CalcEmtiz()
        {
        }
    }
}
