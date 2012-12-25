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
        PersonDTO GetPerson(string name);


        [OperationContract]
        void AddPerson(PersonDTO personDto);

        [OperationContract]
        void AddCustomer(string melliIdentity, string no, float portion);                    

        // TODO: Add your service operations here
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "Valiasr.Service.ContractType".

    [DataContract]
    public class PersonDTO
    {
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
        public string MelliIdentity { get; set; }
        [DataMember]
        public string HeadMelliIdentity { get; set; }
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
        public string PostIdentity { get; set; }
    }
}
