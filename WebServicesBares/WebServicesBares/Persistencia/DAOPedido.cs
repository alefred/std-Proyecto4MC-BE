using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebServicesBares.Dominio;

namespace WebServicesBares.Persistencia
{
    public class DAOPedido
    {
        private static string cadenaconexion = ConfigurationManager.ConnectionStrings["CnxBDAppBar"].ToString();

        public List<EOrder> Listar(string busqueda, string local, string Valor, string fecha)
        {
            List<EOrder> lista = new List<EOrder>();

            string sql = "";
            if (busqueda.Equals("1")) //Listar
                sql = " SELECT SalesOrderId, UserId, PubId, OrderDate, Status, WaitTime, AtentionTime " +
                        " FROM SalesOrders " +
                        " WHERE convert(nvarchar(8), orderDate, 112) = " + fecha +
                        " AND PubId = " + local;
            else if (busqueda.Equals("2")) //Por ID
                sql = " SELECT SalesOrderId, UserId, PubId, OrderDate, Status, WaitTime, AtentionTime " +
                        " FROM SalesOrders " +
                        " WHERE SalesOrderId = " + Valor;
            else if (busqueda.Equals("3")) //Por Estado              
                sql = " SELECT SalesOrderId, UserId, PubId, OrderDate, Status, WaitTime, AtentionTime " +
                        " FROM SalesOrders " +
                        " WHERE Status = " + Valor +
                        " AND PubId = " + local;

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
                                EOrder ve = new EOrder();

                                ve.id = Convert.ToInt32(dr[0]);
                                ve.userId = Convert.ToInt32(dr[1]);
                                ve.pubId = Convert.ToInt32(dr[2]);
                                ve.orderDate = Convert.ToDateTime(dr[3]);
                                ve.status = dr[4].ToString();
                                ve.waitTime = dr[5].ToString();
                                ve.attentionTime = dr[6].ToString();
                                lista.Add(ve);
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

        public EOrder Insertar(EOrder venta)
        {
            string sql = " INSERT INTO SalesOrders(UserId, PubId, OrderDate, Status, WaitTime) " +
                " VALUES(@UserId, @PubId, GETDATE(), '1', @WaitTime); Select SCOPE_IDENTITY(); ";

            EOrder pedidoCreado = null;
            int idventa = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(cadenaconexion))
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand(sql, con))
                    {
                        com.Parameters.Add(new SqlParameter("@UserId", venta.userId));
                        com.Parameters.Add(new SqlParameter("@PubId", venta.pubId));
                        com.Parameters.Add(new SqlParameter("@WaitTime", venta.waitTime));
                        idventa = Convert.ToInt32(com.ExecuteScalar());
                    }

                    pedidoCreado = GetOrderById(idventa);
                }
                return pedidoCreado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EOrder Update(EOrder venta)
        {
            EOrder pedido;
            string qSql = " UPDATE SalesOrders " +
                            " SET Status = 2, AtentionTime = @attentionTime " +
                            " WHERE SalesOrderId = @id";
            try
            {
                using (SqlConnection con = new SqlConnection(cadenaconexion))
                {
                    using (SqlCommand cmd = new SqlCommand(qSql, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@attentionTime", venta.attentionTime));
                        cmd.Parameters.Add(new SqlParameter("@id", venta.id));

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                pedido = GetOrderById(venta.id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pedido;
        }

        public EOrder Anular(int ventaId)
        {
            string qSql = "UPDATE SalesOrders SET Status = 3 where SalesOrderId = @id";

            EOrder pedido; ;

            try
            {
                using (SqlConnection con = new SqlConnection(cadenaconexion))
                {
                    using (SqlCommand cmd = new SqlCommand(qSql, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@id", ventaId));

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                pedido = GetOrderById(ventaId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pedido;
        }

        public EOrder GetOrderById(int id)
        {
            EOrder order = null;

            string sql = " Select SO.SalesOrderId, So.UserId, So.PubId, So.OrderDate, So.Status, So.WaitTime, So.AtentionTime " +
            " from SalesOrders SO Where SO.SalesOrderId = " + id.ToString();

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
                                order = new EOrder();

                                order.id = Convert.ToInt32(dr[0]);
                                order.userId = Convert.ToInt32(dr[1]);
                                order.pubId = Convert.ToInt32(dr[2]);
                                order.orderDate = Convert.ToDateTime(dr[3]);
                                order.status = dr[4].ToString();
                                order.waitTime = dr[5].ToString();
                                order.attentionTime = (dr[6] == DBNull.Value ? null : (string)dr[6]);
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

            return order;
        }


    }
}