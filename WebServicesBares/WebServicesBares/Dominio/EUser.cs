using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebServicesBares.Dominio
{
    [DataContract]
    public class EUser
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string lastName { get; set; }
        [DataMember]
        public string firstName { get; set; }
        [DataMember]
        public string fullName { get { return string.Concat(lastName, ", ", firstName); } }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string phoneNumber { get; set; }
        [DataMember]
        public DateTime postDate { get; set; }
        [DataMember]
        public string documentNumber { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public string type { get; set; }


    }
}