namespace Valiasr.Domain.Model
{
    using System;

    public class AccountActivity
    {
        public Guid Id { get; set; }
        public string AccountCode { get; set; }
        public short YearOf { get; set; }
        public int RegDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public short Category { get; set; }
        public int DestinationShobehCode { get; set; }
        public short NaghdiEnteghali { get; set; }
        public int UsanceDate { get; set; }
        public int Numerator { get; set; }
        public int GroupId { get; set; }
        public int PageNo { get; set; }
        public string Seri { get; set; }
        public int Serial { get; set; }
        public int Destination { get; set; }
        public int PersonCode { get; set; }
        public int SanadTime { get; set; }
        public int Radif { get; set; }
        public decimal Balance { get; set; }
        public int EntryFormatType { get; set; }
        public int AssistantCode { get; set; }
        public int ShobehCode { get; set; }
        public virtual BankAccount BankAccount { get; set; }
    }
}
