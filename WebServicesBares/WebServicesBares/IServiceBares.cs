using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WebServicesBares.Dominio;

namespace WebServicesBares
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServiceBares
    {
        #region "Pedido"

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Order?Gbusqueda={busqueda}&Gvalor={Valor}&Gfecha={fecha}&Glocal={local}", ResponseFormat = WebMessageFormat.Json)]
        List<EOrder> ListarPedido(string busqueda, string Valor, string fecha, string local);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Order", ResponseFormat = WebMessageFormat.Json)]
        EOrder InsertarPedido(EOrder beventa);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "Order", ResponseFormat = WebMessageFormat.Json)]
        EOrder UpdatePedido(EOrder beventa);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "Order/{codigo}", ResponseFormat = WebMessageFormat.Json)]
        EOrder AnularPedido(string codigo);

        #endregion

        #region "Detalle Pedido"

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "OrderDetail?orderId={orderId}", ResponseFormat = WebMessageFormat.Json)]
        List<EOrderDetail> ListarDetallePedido(string orderId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "OrderDetail", ResponseFormat = WebMessageFormat.Json)]
        int insertarDetallePedido(EOrderDetail beventadetalle);

        #endregion

        #region "Usuario"

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "User?username={username}&password={password}&type={type}", ResponseFormat = WebMessageFormat.Json)]
        EUser Login(string username, string password, string type);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "User", ResponseFormat = WebMessageFormat.Json)]
        EUser InsertarUsuario(EUser oUser);

        #endregion

        #region "Local"

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Pub?name={name}", ResponseFormat = WebMessageFormat.Json)]
        List<EPub> ListarLocal(string name);

        #endregion

        #region "Producto"

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Product?pub={local}&type={tipo}", ResponseFormat = WebMessageFormat.Json)]
        List<EProduct> ListarProducto(string local, string tipo);

        #endregion

    }
}
