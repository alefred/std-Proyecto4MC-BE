using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFFLORES.TestRest
{
    public class Pedido
    {

         public int idPedido { get; set; }

        public int idUsuario { get; set; }

        public int idLocal { get; set; }

        public DateTime fechaPedido { get; set; }

        public string estadoPedido { get; set; }

        public string tiempoEsperado { get; set; }

        public string tiempoAtendido { get; set; }
    }
}
