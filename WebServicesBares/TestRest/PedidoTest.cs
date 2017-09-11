﻿﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Text;

namespace CFFLORES.TestRest
{
    [TestClass]
    public class PedidoTest
    {

        [TestMethod]
        public void TestListar()
        {

            string busqueda = "1";
            string valor = "1";
            string fecha = "20170911";
            string local = "1";
            try
            {

                string URLAuth = "http://localhost:11110/ServiceBares.svc/Pedido?Gbusqueda=" + busqueda.ToString() + "&Gvalor=" + valor.ToString() + "&Gfecha=" + fecha.ToString() + "&Glocal=" + local.ToString();

                HttpWebRequest req = (HttpWebRequest)WebRequest.
                    Create(URLAuth);
                req.Method = "GET";

                req.ContentType = "application/json";

                var res = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(res.GetResponseStream());
                string clienteJson = reader.ReadToEnd();
                JavaScriptSerializer JsonConvert = new JavaScriptSerializer();
                List<Pedido> registros = new List<Pedido>();
                registros = JsonConvert.Deserialize<List<Pedido>>(clienteJson);

                foreach (var value in registros)
                {
                    Assert.AreEqual(valor, value.idPedido);
                }

            }
            catch (WebException ex)
            {
                HttpStatusCode code = ((HttpWebResponse)ex.Response).StatusCode;
                string message = ((HttpWebResponse)ex.Response).StatusDescription;
                StreamReader reader = new StreamReader(ex.Response.GetResponseStream());
                string error = reader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                string mensaje = js.Deserialize<string>(error);
                if (busqueda.Equals("2"))
                    Assert.AreEqual("La Búsqueda por DNI debe contener 8 Caracteres", mensaje);
                else if (busqueda.Equals("3"))
                    Assert.AreEqual("La Búsqueda por Serie debe contener 5 Caracteres", mensaje);
                else
                    Assert.AreEqual("No Existe la Venta según los parámetros ingresados", mensaje);
            }
        }
        /*[TestMethod]
        public void TestModificar()
        {
      
             Estados:
             0: Venta
             1: Contabilizado
             2: Anulado
        
            string idcliente = "9";
            string estado = "2";// 0: Venta; 2:Anular

            string postdata = "{\"IdVenta\":\"" + idcliente + "\",\"Estado\":\"" + estado + "\"}";

            try
            {
                byte[] data = Encoding.UTF8.GetBytes(postdata);
                string URLAuth = "http://localhost:24832/Venta.svc/Ventas";

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URLAuth);
                req.Method = "PUT";
                req.ContentLength = data.Length;
                req.ContentType = "application/json";
                var reqStream = req.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                var res = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(res.GetResponseStream());
                string clienteJson = reader.ReadToEnd();
                JavaScriptSerializer JsonConvert = new JavaScriptSerializer();
                List<Pedido> registros = new List<Pedido>();
                registros = JsonConvert.Deserialize<List<Pedido>>(clienteJson);

                foreach (var value in registros)
                {
                    Assert.AreEqual(idcliente, value.IdVenta.ToString());
                }

            }
            catch (WebException ex)
            {
                HttpStatusCode code = ((HttpWebResponse)ex.Response).StatusCode;
                string message = ((HttpWebResponse)ex.Response).StatusDescription;
                StreamReader reader = new StreamReader(ex.Response.GetResponseStream());
                string error = reader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                string mensaje = js.Deserialize<string>(error);
                if (estado.Equals("1"))
                    Assert.AreEqual("No se puede Anular una Venta con estado Contabilizado", mensaje);
                else if (estado.Equals("2"))
                    Assert.AreEqual("No se puede anular una venta ya anulada", mensaje);
                else if (estado.Equals("3"))
                    Assert.AreEqual("No Existe la Venta según los parámetros ingresados", mensaje);

            }

        }
        [TestMethod]
        public void TestInsertar()
        {
            string strdni = "66666666";
            string strtipdoc = "FACTURA";
            string strnrodoc = "00007";
            string strserie = "003";
            double doumonto = Convert.ToDouble("150.50");
            string strcliente = "Manuel";
            string strformapago = "EFECTIVO";

            string postdata = "{\"Dni\":\"" + strdni +
                "\",\"TipoDoc\":\"" + strtipdoc +
                "\",\"NroDoc\":\"" + strnrodoc +
                "\",\"Serie\":\"" + strserie +
                "\",\"Monto\":\"" + doumonto +
                "\",\"Estado\":\"" + 0 +
                "\",\"Cliente\":\"" + strcliente +
                "\",\"FormaPago\":\"" + strformapago + "\"}";

            
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(postdata);
                string URLAuth = "http://localhost:24832/Venta.svc/Ventas";

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URLAuth);
                req.Method = "POST";
                req.ContentLength = data.Length;
                req.ContentType = "application/json";
                var reqStream = req.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                var res = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(res.GetResponseStream());
                string clienteJson = reader.ReadToEnd();
                JavaScriptSerializer JsonConvert = new JavaScriptSerializer();
                int registros;
                registros = JsonConvert.Deserialize<int>(clienteJson);



            }
            catch (WebException ex)
            {
                HttpStatusCode code = ((HttpWebResponse)ex.Response).StatusCode;
                string message = ((HttpWebResponse)ex.Response).StatusDescription;
                StreamReader reader = new StreamReader(ex.Response.GetResponseStream());
                string error = reader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                string mensaje = js.Deserialize<string>(error);

                    Assert.AreEqual("No se puede Anular una Venta con estado Contabilizado", mensaje);


            }
        }

        */


        [TestMethod]
        public void TestGrabarPedido()
        {
            Pedido pedido = new Pedido()
            {
                idLocal = 1,
                idUsuario = 1,
                estadoPedido = "1",
                tiempoEsperado = "05:00"
            };

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string postdata = serializer.Serialize(pedido);
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest req = (HttpWebRequest)WebRequest
                .Create("http://localhost:11110/ServiceBares.svc/Pedido");
            req.Method = "POST";
            req.ContentLength = data.Length;
            req.ContentType = "application/json";
            var reqStream = req.GetRequestStream();
            reqStream.Write(data, 0, data.Length);
            HttpWebResponse res = null;
            int pedidoInsertado = -1;
            try
            {
                res = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(res.GetResponseStream());
                string resultJson = reader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                pedidoInsertado = js.Deserialize<int>(resultJson);

                Assert.AreNotEqual(pedidoInsertado, 0);
            }
            catch (WebException e)
            {
                HttpStatusCode code = ((HttpWebResponse)e.Response).StatusCode;
                string message = ((HttpWebResponse)e.Response).StatusDescription;
                StreamReader reader = new StreamReader(e.Response.GetResponseStream());
                string error = reader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                string mensaje = js.Deserialize<string>(error);
                Assert.AreEqual("", mensaje);
            }


        }

        [TestMethod]
        public void TestActualizarPedido()
        {
            Pedido pedido = new Pedido()
            {
                idPedido = 3,
                tiempoAtendido = "05:00"
            };

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string postdata = serializer.Serialize(pedido);
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest req = (HttpWebRequest)WebRequest
                .Create("http://localhost:11110/ServiceBares.svc/Pedido");
            req.Method = "PUT";
            req.ContentLength = data.Length;
            req.ContentType = "application/json";
            var reqStream = req.GetRequestStream();
            reqStream.Write(data, 0, data.Length);
            HttpWebResponse res = null;
            int pedidoActualizado = -1;
            try
            {
                res = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(res.GetResponseStream());
                string resultJson = reader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                pedidoActualizado = js.Deserialize<int>(resultJson);

                Assert.AreNotEqual(pedidoActualizado, 0);
            }
            catch (WebException e)
            {
                HttpStatusCode code = ((HttpWebResponse)e.Response).StatusCode;
                string message = ((HttpWebResponse)e.Response).StatusDescription;
                StreamReader reader = new StreamReader(e.Response.GetResponseStream());
                string error = reader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                string mensaje = js.Deserialize<string>(error);
                Assert.AreEqual("", mensaje);
            }


        }

        [TestMethod]
        public void TestAnularPedido()
        {
            int idPedido = 3;

            HttpWebRequest req = (HttpWebRequest)WebRequest
                .Create("http://localhost:11110/ServiceBares.svc/Pedido/" + idPedido.ToString());
            req.Method = "DELETE";
            HttpWebResponse res = null;
            int pedidoAnulado = -1;
            try
            {
                res = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(res.GetResponseStream());
                string resultJson = reader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                pedidoAnulado = js.Deserialize<int>(resultJson);

                Assert.AreNotEqual(pedidoAnulado, 0);
            }
            catch (WebException e)
            {
                HttpStatusCode code = ((HttpWebResponse)e.Response).StatusCode;
                string message = ((HttpWebResponse)e.Response).StatusDescription;
                StreamReader reader = new StreamReader(e.Response.GetResponseStream());
                string error = reader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                string mensaje = js.Deserialize<string>(error);
                Assert.AreEqual("", mensaje);
            }


        }
    }
}
