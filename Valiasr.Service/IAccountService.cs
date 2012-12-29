using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Valiasr.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IAccountService
    {
        [OperationContract]
        void AddGeneralAccount(GeneralAccountDto generalAccountDto);

        [OperationContract]
        void AddIndexAccount(IndexAccountDto indexAccountDto);

        [OperationContract]
        void AddAccount<T>(T obj);

        [OperationContract]
        bool CanBardasht(string accountNo, double amount );  

        [OperationContract]
        bool Bardasht(string account, double amount);

        [OperationContract]
        ICollection<CustomerDto> GetCustomerByAccountNo(string accountNo);

        // TODO: Add your service operaGetCustomerByAccountNotions here
    }

    public class GeneralAccountDto
    {
        [DataMember]
        public int Code { get; set; }        

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int Category { get; set; }
    }


    public class IndexAccountDto
    {
        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public int GeneralAccountCode { get; set; }

        [DataMember]
        public int Indexer { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public bool HaveAccounts { get; set; }        
    }


    public class CustomerDto
    {
        [DataMember]
        public string No { get; set; }

        [DataMember]
        public bool HagheBardasht { get; set; }

        [DataMember]
        public float Portion { get; set; }    
    }
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "Valiasr.Service.ContractType".
}
