using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WebServicesBares.Dominio;
using WebServicesBares.Persistencia;

namespace WebServicesBares
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class    Pedido : IServiceBares
    {
        private DAOPedido daopedido = new DAOPedido();
        private DAOUsuario daoUsuario = new DAOUsuario();
        private DAOProducto daoProducto = new DAOProducto();
        private DAOLocal daoLocal = new DAOLocal();

        #region "Pedido"
        public List<EPedido> ListarPedido(string busqueda, string Valor, string fecha, string local)
        {
            /*
             busqueda:
             1: Listar
             2: Por Dni
             3: Por Serie
             4: Por Id
             */
            //if (String.IsNullOrEmpty(busqueda)) busqueda = "1";
            //if (String.IsNullOrEmpty(Valor)) Valor = "1";
            //throw new WebFaultException<string>("La Búsqueda por DNI debe contener 8 Caracteres", HttpStatusCode.InternalServerError);
           /* if (busqueda.Equals("2") && Valor.Length != 8)//Busqueda por DNI
            {
                throw new WebFaultException<string>("La Búsqueda por DNI debe contener 8 Caracteres", HttpStatusCode.InternalServerError);
            }

            if (busqueda.Equals("3") && Valor.Length != 5)//Busqueda por NroVenta
            {
                throw new WebFaultException<string>("La Búsqueda por Serie debe contener 5 Caracteres", HttpStatusCode.InternalServerError);
            }
            */
            List<EPedido> obobVenta = new List<EPedido>();
            obobVenta = daopedido.Listar(busqueda, local, Valor, fecha);

            if (obobVenta.Count == 0)
            {
                throw new WebFaultException<string>("No Existe la Venta según los parámetros ingresados", HttpStatusCode.InternalServerError);

            }



            return obobVenta;


        }

        public int InsertarPedido(EPedido beventa)
        {
            try
            {
                if (beventa == null)
                    return 0;

                int idventa;
                idventa = daopedido.Insertar(beventa);
                return idventa;
            }
            catch (WebException ex)
            {
                throw new WebFaultException<string>(ex.ToString(), HttpStatusCode.InternalServerError);
            }
        }

        public int UpdatePedido(EPedido beventa)
        {
            try
            {
                if (beventa == null)
                    return 0;

                int iRowsUpdate = -1;
                iRowsUpdate = daopedido.Update(beventa);
                return iRowsUpdate;
            }
            catch (WebException ex)
            {
                throw new WebFaultException<string>(ex.ToString(), HttpStatusCode.InternalServerError);
            }
        }

        public int AnularPedido(string codigo)
        {
            try
            {
                int id = 0;

                int.TryParse(codigo, out id);

                if (id == 0)
                    return 0;

                int idAnulado = -1;
                idAnulado = daopedido.Anular(id);
                return idAnulado;
            }
            catch (WebException ex)
            {
                throw new WebFaultException<string>(ex.ToString(), HttpStatusCode.InternalServerError);
            }
        }

        #endregion

        public List<EUsuario> ListarUsuario(string busqueda)
        {
            List<EUsuario> oboUsuario = new List<EUsuario>();
            oboUsuario = daoUsuario.Listar(busqueda);

            if (oboUsuario.Count == 0)
            {
                throw new WebFaultException<string>("No Existe el Usuario según los parámetros ingresados", HttpStatusCode.InternalServerError);

            }



            return oboUsuario;
        }

        public int InsertarUsuario(EUsuario beusuario)
        {

            try
            {
                if (beusuario == null)
                    return 0;

                if (String.IsNullOrEmpty(beusuario.nroDocumento))
                {
                    throw new WebFaultException<string>("Debe ingresar el Cliente", HttpStatusCode.InternalServerError);

                }

                int idusuario;
                idusuario = daoUsuario.Insertar(beusuario);
                return idusuario;
            }
            catch (WebException ex)
            {
                throw new WebFaultException<string>(ex.ToString(), HttpStatusCode.InternalServerError);

            }


        }

        public List<EProducto> ListarProducto(string local)
        {
            List<EProducto> oboProducto = new List<EProducto>();
            oboProducto = daoProducto.Listar(local);

            if (oboProducto.Count == 0)
            {
                throw new WebFaultException<string>("No Existe hay PRoductos del Local", HttpStatusCode.InternalServerError);

            }



            return oboProducto;
        }


        public List<ELocal> ListarLocal()
        {
            List<ELocal> oboLocal= new List<ELocal>();
            oboLocal = daoLocal.Listar();

            if (oboLocal.Count == 0)
            {
                throw new WebFaultException<string>("No Existe hay Locales del Local", HttpStatusCode.InternalServerError);

            }



            return oboLocal;
        }


    }
}
