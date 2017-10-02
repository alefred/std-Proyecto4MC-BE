using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebServicesBares.Dominio
{
    [DataContract]
    public class EOrderDetail
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int orderId { get; set; }
        [DataMember]
        public int productId { get; set; }
        [DataMember]
        public double unitPrice { get; set; }
        [DataMember]
        public int quantity { get; set; }
    }
}