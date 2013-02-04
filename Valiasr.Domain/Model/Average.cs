using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valiasr.Domain.Model
{
    public class Average
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual decimal MakeEmtiaze(int fromDate, int toDate)
        {
            return 0;
        }
    }

    public class NormAverage : Average
    {
        public NormAverage()
        {
            Id = 1;
            Name = "NormAverage";
        }
        public override decimal MakeEmtiaze(int fromDate, int toDate)
        {
            return 1;
        }
    }

    public class AverageWithMin : Average
    {
        public AverageWithMin()
        {
            Name = "AverageWithMin";
        }
        public override decimal MakeEmtiaze(int fromDate, int toDate)
        {
            return 2;
        }
    }

    public class AverageWithHoliday : Average
    {
        public override decimal MakeEmtiaze(int fromDate, int toDate)
        {
            return 3;
        }
    }
}
