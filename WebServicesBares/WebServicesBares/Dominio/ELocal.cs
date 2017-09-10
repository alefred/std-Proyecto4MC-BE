using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebServicesBares.Dominio
{
    [DataContract]
    public class ELocal
    {
        [DataMember]
        public int idLocal { get; set; }
        [DataMember]
        public string nombreLocal { get; set; }
        [DataMember]
        public string ruc { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string UbiLonguitud { get; set; }
        [DataMember]
        public string UbiLatitud { get; set; }
    }
}