
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebServicesBares.Dominio
{
    [DataContract]
    public class EProduct
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public EPub pub { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string image { get; set; }
        [DataMember]
        public double price { get; set; }
        [DataMember]
        public TimeSpan overallTime { get; set; }


    }
}