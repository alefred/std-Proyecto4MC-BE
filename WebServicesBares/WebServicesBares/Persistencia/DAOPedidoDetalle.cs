using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebServicesBares.Dominio;

namespace WebServicesBares.Persistencia
{
    public class DAOPedidoDetalle
    {
        private static string cadenaconexion = ConfigurationManager.ConnectionStrings["CnxBDAppBar"].ToString();

        public int Insertar(EOrderDetail ventadetalle)
        {
            string sql = " INSERT INTO SalesOrderDetails(SalesOrderId, ProductId, UnitPrice, Quantity) " +
            " VALUES(@SalesOrderId, @ProductId, @UnitPrice, @Quantity) ";

            int rowsInsert = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(cadenaconexion))
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand(sql, con))
                    {
                        com.Parameters.Add(new SqlParameter("@SalesOrderId", ventadetalle.orderId));
                        com.Parameters.Add(new SqlParameter("@ProductId", ventadetalle.productId));
                        com.Parameters.Add(new SqlParameter("@UnitPrice", ventadetalle.unitPrice));
                        com.Parameters.Add(new SqlParameter("@Quantity", ventadetalle.quantity));

                        rowsInsert = com.ExecuteNonQuery();
                    }

                }
                return rowsInsert;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EOrderDetail> GetDetalleByOrderId(int id)
        {

            List<EOrderDetail> lista = new List<EOrderDetail>(); ;

            string sql = " SELECT SOD.SalesOrderDetailId,SOD.SalesOrderId,SOD.ProductId,SOD.UnitPrice,SOD.Quantity " +
            " FROM SalesOrderDetails SOD " +
            " Where SOD.SalesOrderId = " + id.ToString();

            try
            {
                using (SqlConnection con = new SqlConnection(cadenaconexion))
                {
                    using (SqlCommand com = new SqlCommand(sql, con))
                    {
                        con.Open();
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                EOrderDetail detalle = new EOrderDetail();

                                detalle.id = Convert.ToInt32(dr[0]);
                                detalle.orderId = Convert.ToInt32(dr[1]);
                                detalle.productId = Convert.ToInt32(dr[2]);
                                detalle.unitPrice = Convert.ToDouble(dr[3]);
                                detalle.quantity = Convert.ToInt32(dr[4]);

                                lista.Add(detalle);
                            }

                            dr.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lista;
        }

    }
}