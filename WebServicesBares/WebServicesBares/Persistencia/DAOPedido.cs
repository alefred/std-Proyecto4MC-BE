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

        public List<EPedido> Listar(string busqueda, string local, string Valor, string fecha)
        {
            List<EPedido> lista = new List<EPedido>();

            string sql = "";
            if (busqueda.Equals("1")) //Listar
                sql = "SELECT * FROM Pedido where convert(nvarchar(8), fechaPedido, 112) = " + fecha + " and idLocal = " + local;
            else if (busqueda.Equals("2")) //Por ID
                sql = "SELECT * FROM Pedido where  idPedido = " + Valor;
            else if (busqueda.Equals("3")) //Por Estado
                sql = "SELECT * FROM Pedido where  estadoPedido = " + Valor  + " and idLocal = " + local; 

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
                                EPedido ve = new EPedido();

                                ve.idPedido = Convert.ToInt32(dr[0]);
                                ve.idUsuario = Convert.ToInt32(dr[1]);
                                ve.idLocal = Convert.ToInt32(dr[2]);
                                ve.fechaPedido = Convert.ToDateTime(dr[3]);
                                ve.estadoPedido = dr[4].ToString();
                                ve.tiempoEsperado = dr[5].ToString();
                                ve.tiempoAtendido = dr[6].ToString();
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

        public int Insertar(EPedido venta)
        {
            string sql = "insert into Pedido ([idUsuario],[idLocal],[fechaPedido],[estado],[tiempoEsperado]) " +
                "values (@idUsuario, @idLocal, GETDATE(), @estadoPedido, @tiempoEsperado)";

            int idventa = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(cadenaconexion))
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand(sql, con))
                    {
                        com.Parameters.Add(new SqlParameter("@idUsuario", venta.idUsuario));
                        com.Parameters.Add(new SqlParameter("@idLocal", venta.idLocal));
                        com.Parameters.Add(new SqlParameter("@estadoPedido", venta.estadoPedido));
                        com.Parameters.Add(new SqlParameter("@tiempoEsperado", venta.tiempoEsperado));
                        com.ExecuteNonQuery();

                    }

                    using (SqlCommand com = new SqlCommand("select max(idPedido) from pedido", con))
                    {
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                idventa = Convert.ToInt32(dr[0]);
                            }
                            dr.Close();
                        }

                    }

                }
                return idventa;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int Update(EPedido venta)
        {
            string qSql = "update pedido set estado = 2, tiempoAtendido =  @tiempoAtendido where idpedido = @id";
            int iRowsModified = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(cadenaconexion))
                {
                    using (SqlCommand cmd = new SqlCommand(qSql, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@tiempoAtendido", venta.tiempoAtendido));
                        cmd.Parameters.Add(new SqlParameter("@id", venta.idPedido));

                        con.Open();
                        iRowsModified = cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iRowsModified;
        }

        public int Anular(int ventaId)
        {
            string qSql = "update pedido set estado = 3 where idpedido = @id";
            int iRowsModified = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(cadenaconexion))
                {
                    using (SqlCommand cmd = new SqlCommand(qSql, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@id", ventaId));

                        con.Open();
                        iRowsModified = cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iRowsModified;
        }

    }
}