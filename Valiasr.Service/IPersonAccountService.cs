using System;

namespace Valiasr.Service
{
    using System.Runtime.Serialization;
    using System.ServiceModel;

    [ServiceContract]
    public interface IPersonAccountService
    {
        /// <summary>
        /// person operation contract
        /// </summary>
        /// <param name="nationalIdentity"></param>
        /// <returns></returns>
        [OperationContract]
        PersonDto GetPersonByNationalIdentity(string nationalIdentity);

        [OperationContract]
        PersonDto GetPersonById(Guid id);

        [OperationContract]
        string AddPerson(PersonDto personDto);

        [OperationContract]
        string UpdatePerson(PersonDto personDto);

        [OperationContract]
        string RemovePerson(Guid id);

        [OperationContract]
        string AddCustomerToAccount(Guid accountId , CustomerDto customerDto);

        [OperationContract]
        string AddLawyerToAccount(Guid accountId , LawyerDto lawyerDto);


        /// <summary>
        /// account operation contract
        /// </summary>
        /// <param name="generalAccountDto"></param>
        [OperationContract]
        string AddGeneralAccount(GeneralAccountDto generalAccountDto);

        [OperationContract]
        string RemoveGeneralAccount(Guid id);

        [OperationContract]
        GeneralAccountDto GetGeneralAccount(int code);

        [OperationContract]
        string AddIndexAccount(IndexAccountDto indexAccountDto);

        [OperationContract]
        string RemoveIndexAccount(Guid id);

        [OperationContract]
        IndexAccountDto GetIndexAccount(string code);

        [OperationContract]
        string AddAccount(AccountDto accountDto);

   //     [OperationContract]
 //       bool CanBardasht(string accountNo, double amount);

     //   [OperationContract]
       // bool Bardasht(string account, double amount);

       // [OperationContract]
        //ICollection<CustomerDto> GetCustomerByAccountNo(string accountNo);
    }

    /// <summary>
    /// person data contract
    /// </summary>
    [DataContract]
    public class PersonDto
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public int CustomerId { get; set; }
        [DataMember]
        public int ShobehCode { get; set; }
        [DataMember]
        public string Firstname { get; set; }
        [DataMember]
        public string Lastname { get; set; }
        [DataMember]
        public string FatherName { get; set; }
        [DataMember]
        public string CretyId { get; set; }
        [DataMember]
        public string CretySerial { get; set; }
        [DataMember]
        public string Sadereh { get; set; }
        [DataMember]
        public int BirthDate { get; set; }
        [DataMember]
        public string NationalIdentity { get; set; }
        [DataMember]
        public string HeadNationalIdentity { get; set; }
        [DataMember]
        public string JobName { get; set; }
        [DataMember]
        public short JobKind { get; set; }
        [DataMember]
        public decimal Salary { get; set; }
        [DataMember]
        public string IndivOrOrgan { get; set; }
        [DataMember]
        public string HomeAddress { get; set; }
        [DataMember]
        public string WorkAddress { get; set; }
        [DataMember]
        public string HomeTelno { get; set; }
        [DataMember]
        public string OfficeTelNo { get; set; }
        [DataMember]
        public string Mobile { get; set; }
        [DataMember]
        public string PostalIdentity { get; set; }
    }

    [DataContract]
    public class CustomerDto
    {
        [DataMember]
        public Guid PersonId { get; set; }

        [DataMember]
        public string No { get; set; }

        [DataMember]
        public bool HagheBardasht { get; set; }

        [DataMember]
        public float Portion { get; set; }
    }

    [DataContract]
    public class LawyerDto
    {
        [DataMember]
        public Guid PersonId { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }
    }

    /// <summary>
    /// account data contract
    /// </summary>
    [DataContract]
    public class GeneralAccountDto
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public int Code { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int Category { get; set; }
    }

    [DataContract]
    public class IndexAccountDto
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid GeneralAccountId { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public int GeneralAccountCode { get; set; }

        [DataMember]
        public int RowId { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public bool HaveAccounts { get; set; }
    }

    [DataContract]
    public class AccountDto
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public Guid IndexAccountId { get; set; }

        [DataMember]
        public string IndexAccountCode { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public int RowId { get; set; }

        [DataMember]
        public string No { get; set; }

        [DataMember]
        public double Balance { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
