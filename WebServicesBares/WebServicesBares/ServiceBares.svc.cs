using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel.Web;
using WebServicesBares.Dominio;
using WebServicesBares.Persistencia;

namespace WebServicesBares
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ServiceBares : IServiceBares
    {
        private DAOPedido daopedido = new DAOPedido();
        private DAOPedidoDetalle daopedidoDetalle = new DAOPedidoDetalle();
        private DAOUsuario daoUsuario = new DAOUsuario();
        private DAOProducto daoProducto = new DAOProducto();
        private DAOLocal daoLocal = new DAOLocal();

        #region "Pedido"
        public List<EOrder> ListarPedido(string busqueda, string Valor, string fecha, string local)
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
            List<EOrder> obobVenta = new List<EOrder>();
            obobVenta = daopedido.Listar(busqueda, local, Valor, fecha);

            if (obobVenta.Count == 0)
            {
                throw new WebFaultException<string>("No Existe la Venta según los parámetros ingresados", HttpStatusCode.InternalServerError);

            }
            return obobVenta;
        }

        public EOrder InsertarPedido(EOrder beventa)
        {
            EOrder nuevoPedido = null;

            if (beventa == null)
            {
                throw new WebFaultException<string>("Entidad no valida", HttpStatusCode.InternalServerError);
            }

            try
            {

                nuevoPedido = daopedido.Insertar(beventa);
                return nuevoPedido;
            }
            catch (WebException ex)
            {
                throw new WebFaultException<string>(ex.ToString(), HttpStatusCode.InternalServerError);
            }
        }

        public EOrder UpdatePedido(EOrder beventa)
        {
            EOrder pedidoActualizado = null;

            try
            {
                if (beventa == null)
                {
                    throw new WebFaultException<string>("Entidad no valida", HttpStatusCode.InternalServerError);
                }

                pedidoActualizado = daopedido.Update(beventa);
                return pedidoActualizado;
            }
            catch (WebException ex)
            {
                throw new WebFaultException<string>(ex.ToString(), HttpStatusCode.InternalServerError);
            }
        }

        public EOrder AnularPedido(string codigo)
        {
            EOrder pedidoAnulado = null;

            try
            {
                int id = 0;

                if (int.TryParse(codigo, out id))
                {
                    pedidoAnulado = daopedido.Anular(id);
                }

                return pedidoAnulado;
            }
            catch (WebException ex)
            {
                throw new WebFaultException<string>(ex.ToString(), HttpStatusCode.InternalServerError);
            }
        }

        #endregion

        #region "Detalle de pedido"

        public List<EOrderDetail> ListarDetallePedido(string orderId)
        {
            int iorderId = 0;
            try
            {
                List<EOrderDetail> obobVentaDetalle = null;
                if (int.TryParse(orderId, out iorderId))
                {
                    obobVentaDetalle = daopedidoDetalle.GetDetalleByOrderId(iorderId);
                }

                if (obobVentaDetalle.Count == 0)
                {
                    throw new WebFaultException<string>("No Existe el detalle de la venta según los parámetros ingresados", HttpStatusCode.InternalServerError);
                }
                return obobVentaDetalle;
            }
            catch (WebException ex)
            {
                throw new WebFaultException<string>(ex.ToString(), HttpStatusCode.InternalServerError);
            }

        }

        public int insertarDetallePedido(EOrderDetail beventadetalle)
        {
            try
            {
                if (beventadetalle == null)
                    return 0;

                int iRows = -1;
                iRows = daopedidoDetalle.Insertar(beventadetalle);
                return iRows;
            }
            catch (WebException ex)
            {
                throw new WebFaultException<string>(ex.ToString(), HttpStatusCode.InternalServerError);
            }
        }

        #endregion

        #region "Usuario"

        //OK
        public EUser Login(string username, string password, string type)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new WebFaultException<string>("Ingresar usuario", HttpStatusCode.InternalServerError);
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new WebFaultException<string>("Ingresar password", HttpStatusCode.InternalServerError);
            }

            if (string.IsNullOrEmpty(type))
            {
                type = "1";
            }

            EUser usuarioLogueado;
            try
            {
                usuarioLogueado = daoUsuario.Login(username, password, type);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
            return usuarioLogueado;
        }

        public List<EUser> LoginId(string iduser)
        {
            int iiuserid = 0;
            List<EUser> obobUSer = new List<EUser>();
            try
            {
                if (int.TryParse(iduser, out iiuserid))
                {
                    obobUSer.Add(daoUsuario.GetUserById(iiuserid));
                }


            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
            return obobUSer;
        }

        public EUser InsertarUsuario(EUser oUser)
        {

            if (oUser == null)
            {
                throw new WebFaultException<string>("Entidad no valida", HttpStatusCode.InternalServerError);
            }

            if (String.IsNullOrEmpty(oUser.lastName) || String.IsNullOrEmpty(oUser.firstName))
            {
                throw new WebFaultException<string>("Debe ingresar apellidos y nombres", HttpStatusCode.InternalServerError);
            }

            if (String.IsNullOrEmpty(oUser.documentNumber))
            {
                throw new WebFaultException<string>("Debe ingresar el numero de documento", HttpStatusCode.InternalServerError);
            }

            if (String.IsNullOrEmpty(oUser.email))
            {
                throw new WebFaultException<string>("Debe ingresar email válido", HttpStatusCode.InternalServerError);
            }

            try
            {
                EUser usuarioRegistrado;
                usuarioRegistrado = daoUsuario.Insertar(oUser);
                return usuarioRegistrado;
            }
            catch (WebException ex)
            {
                throw new WebFaultException<string>(ex.ToString(), HttpStatusCode.InternalServerError);

            }
        }

        #endregion

        #region "Local"

        public List<EPub> ListarLocal(string name)
        {
            List<EPub> oboLocal = null;

            try
            {
                oboLocal = daoLocal.Listar(name);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }

            return oboLocal;
        }

        #endregion

        #region "Producto"

        public List<EProduct> ListarProducto(string local, string tipo)
        {
            List<EProduct> oboProducto = null;
            int iLocal = 0;


            if (String.IsNullOrEmpty(local))
            {
                throw new WebFaultException<string>("Debe ingresar el local a consultar", HttpStatusCode.InternalServerError);
            }

            if (String.IsNullOrEmpty(tipo))
            {
                throw new WebFaultException<string>("Debe ingresar el tipo de producto", HttpStatusCode.InternalServerError);
            }

            try
            {
                if (int.TryParse(local, out iLocal))
                {
                    oboProducto = daoProducto.Listar(iLocal, tipo);
                }
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }

            return oboProducto;
        }


        public List<EProduct> ListarProductoID(string idproducto)
        {
            List<EProduct> oboProducto = null;
            int iidproducto = 0;




            try
            {
                if (int.TryParse(idproducto, out iidproducto))
                {
                    oboProducto = daoProducto.ListarID(iidproducto);
                }
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }

            return oboProducto;
        }


        #endregion
    }
}
