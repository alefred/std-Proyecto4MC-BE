
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebServicesBares.Dominio
{
    [DataContract]
    public class EProducto
    {
        [DataMember]
        public int idProducto { get; set; }

        [DataMember]
        public int idLocal { get; set; }

        [DataMember]
        public string nombreProducto { get; set; }

        [DataMember]
        public string descripcionProducto { get; set; }

        [DataMember]
        public string tipoProducto { get; set; }

        [DataMember]
        public string imagen { get; set; }

        [DataMember]
        public double precio { get; set; }









    }
}