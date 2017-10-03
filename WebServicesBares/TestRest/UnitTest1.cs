using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using TestRest;
using System.IO;
using System.Web.Script.Serialization;
using System.Text;

namespace Bares.TestRest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestLogin()
        {
            string usuario = "45785421";
            string password = "123";
            string tipo = "1";

            string URLAuth = "http://localhost:15000/ServiceBares.svc/User?username=" + usuario + "&password=" + password + "&type=" + tipo;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URLAuth);
            req.Method = "GET";
            //req.KeepAlive = false;
            EUser registros = null;
            try
            {
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(res.GetResponseStream());
                string stringJson = reader.ReadToEnd();
                JavaScriptSerializer JsonConvert = new JavaScriptSerializer();
                registros = JsonConvert.Deserialize<EUser>(stringJson);

                Assert.IsNotNull(registros);
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
        public void InsertarUsuario()
        {
            //EUser usuario = new EUser()
            //{
            //    lastName = "Carrasco",
            //    firstName = "Julia",
            //    email = "test@gmail.com",
            //    phoneNumber = "987654321",
            //    documentNumber = "45781266",
            //    type = "2",
            //    password = "123"
            //};

            //EOrder pedido = new EOrder() {
            //    userId = 3,
            //    pubId = 1,
            //    waitTime = "00:10:00"
            //};

            EOrderDetail pedido = new EOrderDetail()
            {
                orderId = 2,
                productId = 1,
                unitPrice = 20.00,
                quantity = 2
            };

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string postdata = serializer.Serialize(pedido); ;
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest req = (HttpWebRequest)WebRequest
                .Create("http://localhost:15000/ServiceBares.svc/OrderDetail");
            req.Method = "POST";
            req.ContentLength = data.Length;
            req.ContentType = "application/json";
            var reqStream = req.GetRequestStream();
            reqStream.Write(data, 0, data.Length);
            HttpWebResponse res = null;
            EOrder cat = null;
            try
            {
                res = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(res.GetResponseStream());
                string listaCATJson = reader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                cat = js.Deserialize<EOrder>(listaCATJson);

                Assert.IsNotNull(cat);
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
