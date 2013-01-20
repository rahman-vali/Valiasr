using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valiasr.Domain.Model
{
    using System.Security.Permissions;

    public class LoanRequest:IAggregateRoot
    {
        public LoanRequest()
        {
            Id = Guid.NewGuid();
        }
        public static LoanRequest CreateLoanRequest(Account account ,int reqNo , int date , decimal amount , string description , int duration , string durationType , int lastDate , short indivOrOrgan )
        {

            var LoanRequest = new LoanRequest
                {
                    Id = Guid.NewGuid(),
                    ReqNo = reqNo,
                    LoanRequestDate = date,
                    Amount = amount,
                    Description = description,
                    Duration = duration,
                    DurationType = durationType,
                    IndivOrOrgan = indivOrOrgan,
                    LastDate = lastDate
                };
            return LoanRequest;
        }
        public Guid Id { get; set; }
        public int ReqNo { get; set; }
        public int LoanRequestDate { get; set; }
        public string AccountCode { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int Duration { get; set; }
        public string DurationType { get; set; }
        public int PaymentCount { get; set; }
        public int FingerRegId { get; set; }              
        public int LastDate { get; set; }
        public short RequestKind { get; set; }
        public short IndivOrOrgan { get; set; }
        public virtual Account Account { get; set; }   
        public virtual LoanRequestOkyAssistant LoanRequestOkyAsistant { get; set; }
        public virtual ICollection<RequestAccountAve> RequestAccountAves { get; set; }
        public virtual Loan Loan { get; set; }

        public bool CanBeSaved
        {
            get
            { return true; }
        }

        public bool CanBeDeleted
        {
            get { return true; }
        }
    }

    public class LoanRequestOkyAssistant
    {        
        public Guid Id { get; set; }
        public decimal OkyQty { get; set; }
        public int OkyDuration { get; set; }
        public string OkyDurationType { get; set; }
        public int PaymentCount { get; set; }
        public string OkyAss { get; set; }
        public int OKyDate { get; set; }
        public int RegPerId { get; set; }        
    }

    public class RequestAccountAve
    {
        public Guid Id { get; set; }
        public int ReqNo { get; set; }
        public int fromDate { get; set; }
        public int ToDate { get; set; }
        public string AccountCode { get; set; }
        public decimal DebtQty { get; set; }
        public decimal Average { get; set; }
        public decimal LastBalance { get; set; }
        public int LastDate { get; set; }
        public decimal ConsumedQty { get; set; }
        public virtual Account Account { get; set; }
        public virtual LoanRequest LoanRequest { get; set; }
    }

    public class Loan
    {
        public Guid Id { get; set; }
        public string LoanNo { get; set; }
        public int ReqNo { get; set; }
        public int Date { get; set; }
        public decimal Amount { get; set; }
        public int Duration { get; set; }
        public string DurationType { get; set; }
        public int PaymentCount { get; set; }
        public int FirstPaymentDate { get; set; }
        public float WageRate { get; set; }
        public decimal Wage { get; set; }
        public string Assurance { get; set; }
        public short AssuranceCount { get; set; }
        public int LastPaymentDate { get; set; }
        public string StepDef { get; set; }
        public short StepCount { get; set; }
        public short WagePayment { get; set; }
        public short GrantPercent { get; set; }
        public int DelayDayCount { get; set; }
        public int SpeedyDayCount { get; set; }
        public int DelaMonthCount { get; set; }
        public int LastDate { get; set; }
        public int SuperiorOky { get; set; }
        public int SuperiorId { get; set; }
        public int RegisteredId { get; set; }
        public virtual LoanRequest LoanRequest { get; set; }
        public virtual Account Account { get; set; }
    }
}
