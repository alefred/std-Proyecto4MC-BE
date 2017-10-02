using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebServicesBares.Dominio
{
    [DataContract]
    public class EOrder
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int userId { get; set; }
        [DataMember]
        public int pubId { get; set; }
        [DataMember]
        public DateTime orderDate { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string waitTime { get; set; }
        [DataMember]
        public string attentionTime { get; set; }

    }
}