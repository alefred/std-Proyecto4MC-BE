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
        [WebInvoke(Method = "GET", UriTemplate = "Pedido?Gbusqueda={busqueda}&Gvalor={Valor}&Gfecha={fecha}&Glocal={local}", ResponseFormat = WebMessageFormat.Json)]
        List<EPedido> ListarPedido(string busqueda, string Valor, string fecha, string local);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Pedido", ResponseFormat = WebMessageFormat.Json)]
        int InsertarPedido(EPedido beventa);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "Pedido", ResponseFormat = WebMessageFormat.Json)]
        int UpdatePedido(EPedido beventa);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "Pedido/{codigo}", ResponseFormat = WebMessageFormat.Json)]
        int AnularPedido(string codigo);

        #endregion

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Usuario?Gbusqueda={busqueda}", ResponseFormat = WebMessageFormat.Json)]
        List<EUsuario> ListarUsuario(string busqueda);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Usuario", ResponseFormat = WebMessageFormat.Json)]
        int InsertarUsuario(EUsuario beusuario);


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Producto?GLocal={local}", ResponseFormat = WebMessageFormat.Json)]
        List<EProducto> ListarProducto(string local);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Local", ResponseFormat = WebMessageFormat.Json)]
        List<ELocal> ListarLocal();

    }



}
