namespace Valiasr.Service
{
    using System;
    using System.Linq;

    using Valiasr.DataAccess.Repositories;
    using Valiasr.Domain.Model;

    public partial class PersonAccountService : IPersonAccountService
    {
        public string AddLoanRequest(LoanRequestDto loanRequestDto)
        {
            try
            {
                LoanRequestRepository repository = new LoanRequestRepository();
                loanRequestDto.ReqNo = MakeReqNo(repository, loanRequestDto.Date);
                LoanRequest loanRequest = new LoanRequest();
                Account account =
                    repository.ActiveContext.BankAccounts.OfType<Account>()
                              .FirstOrDefault(a => a.Code == loanRequestDto.AccountCode);
                TranslateLoanRequestDtoToLoanRequest(loanRequestDto, loanRequest);
                loanRequest.Account = account;
                RequestAccountAve requestAccountAve = new RequestAccountAve
                    {
                        AccountCode = "1/0/0",
                        Average = 1,
                        ConsumedQty = 2,
                        DebtQty = 3,
                        fromDate = 13911111,
                        LastBalance = 0,
                        LastDate = 4
                    };
                NormAverage normAverage = new NormAverage();
                Average ave = new Average();
                requestAccountAve.AverageM = normAverage;
                requestAccountAve.Account = account;
                requestAccountAve.LoanRequest = loanRequest;
                loanRequest.RequestAccountAves.Add(requestAccountAve);

                repository.Add(loanRequest);
                return "request added successfully";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        private void TranslateLoanRequestDtoToLoanRequest(LoanRequestDto loanRequestDto, LoanRequest loanRequest)
        {
            loanRequest.ReqNo = loanRequestDto.ReqNo;
            loanRequest.LoanRequestDate = loanRequestDto.Date;
            loanRequest.Amount = loanRequestDto.Amount;
            loanRequest.AccountCode = loanRequestDto.AccountCode;
            loanRequest.Description = loanRequestDto.Description;
            loanRequest.Duration = loanRequestDto.Duration;
            loanRequest.DurationType = loanRequestDto.DurationType;
            loanRequest.PaymentCount = loanRequestDto.PaymentCount;
            loanRequest.LastDate = loanRequestDto.LastDate;
            loanRequest.RequestKind = loanRequestDto.RequestKind;
            loanRequest.IndivOrOrgan = loanRequestDto.IndivOrOrgan;
            loanRequest.FingerRegId = loanRequestDto.FingerRegId;
        }

        private int MakeReqNo(LoanRequestRepository repository, int date)
        {
            int no =
                repository.ActiveContext.LoanRequests.Where(lr => lr.LoanRequestDate == date)
                          .Select(lr => lr.ReqNo)
                          .DefaultIfEmpty(0)
                          .Max() + 1;
            if (no == 1) no = date * 1000 + 1;
            return no;
        }

        public LoanRequestDto GetLoanRequest(int reqNo)
        {
            LoanRequestRepository repository = new LoanRequestRepository();
            LoanRequest loanRequest = repository.ActiveContext.LoanRequests.FirstOrDefault(lr => lr.ReqNo == reqNo);
            return TranslateLoanRequestToLoanRequestDto(loanRequest);

        }

        private LoanRequestDto TranslateLoanRequestToLoanRequestDto(LoanRequest loanRequest)
        {
            if (loanRequest == null)
            {
                LoanRequestDto loanRequestDtoNull = new LoanRequestDto { Description = "record not found" };
                return loanRequestDtoNull;
            }
            LoanRequestDto loanRequestDto = new LoanRequestDto
            {
                ReqNo = loanRequest.ReqNo,
                AccountCode = loanRequest.AccountCode,
                Amount = loanRequest.Amount,
                Date = loanRequest.LoanRequestDate,
                Description = loanRequest.Description,
                Duration = loanRequest.Duration,
                DurationType = loanRequest.DurationType,
                FingerRegId = loanRequest.FingerRegId,
                Id = loanRequest.Id,
                IndivOrOrgan = loanRequest.IndivOrOrgan,
            };
            return loanRequestDto;
        }

        public string UpdaeLoanRequest(LoanRequestDto loanRequestDto)
        {
            try
            {
                Guid id = loanRequestDto.Id;
                LoanRequestRepository repository = new LoanRequestRepository();
                LoanRequest loanRequest =
                    repository.ActiveContext.LoanRequests.Where(lr => lr.Id == id).FirstOrDefault();
                if (loanRequest == null) return "request is not there ";
                this.TranslateLoanRequestDtoToLoanRequest(loanRequestDto, loanRequest);
                repository.Update(loanRequest);
                return "updated successfully";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public string RemoveLoanRequest(int reqNo)
        {
            LoanRequestRepository repository = new LoanRequestRepository();
            LoanRequest loanRequest = repository.ActiveContext.LoanRequests.FirstOrDefault(lr => lr.ReqNo == reqNo);
            if (loanRequest == null) return "record not found to be deleted";

            return "";
        }

        public string AddOrUpdateLoanRequestOkyAssistant(Guid loanRequestId, LoanRequestOkyDto loanRequestOkyDto)
        {
            try
            {
                LoanRequestRepository repository = new LoanRequestRepository();
                LoanRequest loanRequest =
                    repository.ActiveContext.LoanRequests.FirstOrDefault(lr => lr.Id == loanRequestId);
                if (loanRequest == null) return "this request with this Request no :" + loanRequestOkyDto.ReqNo.ToString() + " is not there";
                TranslateLoanRequestOkyDto(loanRequestOkyDto, loanRequest);
                repository.Update(loanRequest);
                return "requestOkyAss added successfully";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }


        private void TranslateLoanRequestOkyDto(LoanRequestOkyDto loanRequestOkyDto, LoanRequest loanRequest)
        {
            loanRequest.LoanRequestOkyAsistant.OKyDate = loanRequestOkyDto.OKyDate;
            loanRequest.LoanRequestOkyAsistant.OkyAss = loanRequestOkyDto.OkyAss;
            loanRequest.LoanRequestOkyAsistant.OkyQty = loanRequestOkyDto.OkyQty;
            loanRequest.LoanRequestOkyAsistant.PaymentCount = loanRequestOkyDto.PaymentCount;
            loanRequest.LoanRequestOkyAsistant.OkyDuration = loanRequestOkyDto.OkyDuration;
            loanRequest.LoanRequestOkyAsistant.OkyDurationType = loanRequestOkyDto.OkyDurationType;
            loanRequest.LoanRequestOkyAsistant.RegPerId = loanRequestOkyDto.RegPerId;
        }

        public string AddRequestAccountAve(Guid loanRequestId, RequestAccountAveDto requestAccountAveDto)
        {
            try
            {
                string accountCode = requestAccountAveDto.AccountCode;
                LoanRequestRepository repository = new LoanRequestRepository();
                LoanRequest loanRequest = repository.ActiveContext.LoanRequests.FirstOrDefault(lr => lr.Id == loanRequestId);
                if (loanRequest == null) return "this request with this Request no :" + requestAccountAveDto.ReqNo.ToString() + " is not there";
                Account account = repository.ActiveContext.BankAccounts.OfType<Account>().FirstOrDefault(a => a.Code == accountCode);
                if (account == null) return "account is not found";
                RequestAccountAve requestAccountAve = new RequestAccountAve();
                this.TranslateRequestAccountAveDtoToRequestAccountAve(requestAccountAveDto, requestAccountAve);
       //         requestAccountAve.Account = account;
                loanRequest.RequestAccountAves.Add(requestAccountAve);
                repository.ActiveContext.SaveChanges();
                return "record added successfully";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        private void TranslateRequestAccountAveDtoToRequestAccountAve(RequestAccountAveDto requestAccountAveDto, RequestAccountAve requestAccountAve)
        {

            requestAccountAve.ReqNo = requestAccountAveDto.ReqNo;
            requestAccountAve.Average = requestAccountAveDto.Average;
            requestAccountAve.fromDate = requestAccountAveDto.fromDate;
            requestAccountAve.ToDate = requestAccountAveDto.ToDate;
            requestAccountAve.AccountCode = requestAccountAveDto.AccountCode;
            requestAccountAve.DebtQty = requestAccountAveDto.DebtQty;
            requestAccountAve.ConsumedQty = requestAccountAveDto.ConsumedQty;
            requestAccountAve.LastBalance = requestAccountAveDto.LastBalance;
            requestAccountAve.LastDate = requestAccountAveDto.LastDate;
        }
    }
}
