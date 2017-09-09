using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebServicesBares.Dominio;

namespace WebServicesBares.Persistencia
{
    public class DAOPedido
    {
        //private string cadenaconexion = "Data Source=LAPTOP-C3204AHJ\\SQLEXPRESS;Initial Catalog=CFFLORESDB;Integrated Security=True";
        private string cadenaconexion = "Data Source=azrdb03.database.windows.net;Initial Catalog=arsDB03;Persist Security Info=True;User ID=alefred;Password=Pa$$w0rd";

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
                    con.Open();
                    using (SqlCommand com = new SqlCommand(sql, con))
                    {
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

            string sql = "insert into Pedido ([idUsuario],[idLocal],[fechaPedido],[estadoPedido],[tiempoEsperado],[tiempoAtendido])                 values (@idUsuario, @idLocal, GETDATE(), @estadoPedido, @tiempoEsperado, @tiempoAtendido)";

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
                        com.Parameters.Add(new SqlParameter("@fechaPedido", venta.fechaPedido));
                        com.Parameters.Add(new SqlParameter("@estadoPedido", venta.estadoPedido));
                        com.Parameters.Add(new SqlParameter("@tiempoEsperado", venta.tiempoEsperado));
                        com.Parameters.Add(new SqlParameter("@tiempoAtendido", venta.tiempoAtendido));
                        com.ExecuteNonQuery();

                    }

                    using (SqlCommand com = new SqlCommand("select max(idventa) from venta", con))
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

        
    }
}