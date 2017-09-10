using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebServicesBares.Dominio
{
    [DataContract]
    public class EUsuario
    {
        [DataMember]
        public int idUsuario { get; set; }

        [DataMember]
        public string nombreCompleto { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public DateTime fechaRegistro { get; set; }
        [DataMember]
        public string nroDocumento { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public string tipoUsuario { get; set; }


    }
}