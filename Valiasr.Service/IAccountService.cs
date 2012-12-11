using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Valiasr.Service
{
    using Valiasr.NewDomain;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IAccountService
    {
        [OperationContract]
        bool CanBardasht(string accountNo, double amount );  

        [OperationContract]
        bool Bardasht(string account, double amount);

        [OperationContract]
        ICollection<CustomerDto> GetCustomerByAccountNo(string accountNo);

        // TODO: Add your service operaGetCustomerByAccountNotions here
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
