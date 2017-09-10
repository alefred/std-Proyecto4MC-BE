using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebServicesBares.Dominio
{
    [DataContract]
    public class EPedido
    {
        [DataMember]
        public int idPedido { get; set; }
        [DataMember]
        public int idUsuario { get; set; }
        [DataMember]
        public int idLocal { get; set; }
        [DataMember]
        public DateTime fechaPedido { get; set; }
        [DataMember]
        public string estadoPedido { get; set; }
        [DataMember]
        public string tiempoEsperado { get; set; }
        [DataMember]
        public string tiempoAtendido { get; set; }

    }
}