using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebServicesBares.Dominio
{
    [DataContract]
    public class EPub
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string ruc { get; set; }
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public string phoneNumber { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string longitude { get; set; }
        [DataMember]
        public string latitude { get; set; }
    }
}