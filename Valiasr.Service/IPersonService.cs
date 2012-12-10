using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Valiasr.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IPersonService
    {

        [OperationContract]
        PersonDto GetPerson(string name);                    


        // TODO: Add your service operations here
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "Valiasr.Service.ContractType".

    [DataContract]
    public class PersonDto
    {
        [DataMember]
        public string Firstname { get; set; }
        [DataMember]
        public string Lastname { get; set; }
    }
}
